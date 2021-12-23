/**
 * @ Author: Akshaya Niraula
 * @ Create Time: 2021-12-22 4:42
 * @ Copyright: Copyright (c) 2021 Akshaya Niraula. See LICENSE for details
 */

using Haestad.Drawing.Domain;
using Haestad.Framework.Application;
using Haestad.Support.Library;
using Haestad.Support.Support;
using Haestad.Support.User;
using OFW.IsolationValveAdder.App.Support;
using OFW.IsolationValveAdder.App.UserControlModel;
using OpenFlows.Water.Application;
using OpenFlows.Water.Domain;
using OpenFlows.Water.Domain.ModelingElements.NetworkElements;
using System.Collections.Generic;

namespace OFW.IsolationValveAdder.App.FormModel
{
    public class ValveAdderFormModel : HaestadFormModel
    {
        #region Constructor
        public ValveAdderFormModel(IApplicationModel appModel)
            : base("ValveAdderFormModel", appModel)
        {
        }
        #endregion

        #region Override Methods
        public override void Dispose()
        {
            ValvePlacementOptionsControlModel.Dispose();
            base.Dispose();
        }
        #endregion

        #region Public Methods
        public void AddIsolationValves(IProgressIndicator pi)
        {
            // Build the map if needed
            if (PipeHasIsoValveMap.Count == 0)
                BuildPipeHasExistingIsoValveMap();

            // in case of repeat run, delete the previously added valves.
            if (IsoValvesAdded.Count > 0)
            {
                pi.AddTask("Removing isolation valves from last run...");
                pi.IncrementTask();
                pi.BeginTask(1);

                IsoValvesAdded.ForEach(v => v.Delete());

                pi.IncrementStep();
                pi.EndTask();
            }

            // in case of re-run
            IsoValvesAdded = new List<IIsolationValve>();
            SkippedPipesDueToDiameterThreshold = new List<IPipe>();
            SkippedPipesDueToExistingIsoValves = new List<IPipe>();
            SkippedPipesDueToLengthThreshold = new List<IPipe>();


            // Add new valves
            pi.AddTask("Creating isolation valves on pipes (where possible)...");
            pi.IncrementTask();
            pi.BeginTask(WaterModel.Network.Pipes.Count);

            foreach (var pipe in WaterModel.Network.Pipes.Elements())
            {
                AddIsolationValvesForPipe(pipe);
                pi.IncrementStep();
            }

            pi.EndTask();


            // Update database cache
            AppManager.ParentFormUIModel.ExecuteCommand(CommandType.UpdateDatabaseCaches);

            // Sync the drawing
            if (IsoValvesAdded.Count > 0)
            {
                AppManager.ParentFormUIModel.LayoutController.SynchronizeWithDatabase(
                    (IGraphicalProject)AppManager.ParentFormModel.CurrentProject, pi);
            }

        }
        public string GetChangeSummary()
        {
            var message = $"Number of Isolation valves created: {IsoValvesAdded.Count}";
            message += $"\n\nNumber of Pipes skipped due to existing Isolation valves: {SkippedPipesDueToExistingIsoValves.Count}";
            message += $"\nNumber of Pipes skipped due to Diameter threshold: {SkippedPipesDueToDiameterThreshold.Count}";
            message += $"\nNumber of Pipes skipped due to Length threshold: {SkippedPipesDueToLengthThreshold.Count}";
            message += $"\nNumber of Pipes skipped due to User-Defined Length: {SkippedPipesDueToUserDefinedLength.Count}";

            return message;
        }
        #endregion

        #region Private Methods
        private void BuildPipeHasExistingIsoValveMap()
        {
            // Initialize the dictionary with false (assuming no isolation valves)
            foreach (var pipe in WaterModel.Network.Pipes.Elements())
                PipeHasIsoValveMap.Add(pipe.Id, false);

            foreach (var isoValve in WaterModel.Network.IsolationValves.Elements())
            {
                var refPipe = isoValve.Input.ReferencedPipe;
                if (refPipe != null)
                    PipeHasIsoValveMap[refPipe.Id] = true;
            }
        }
        private void AddIsolationValvesForPipe(IPipe pipe)
        {
            // if a pipe already has an isolation valve, do not add a new valve
            if (PipeHasIsoValveMap[pipe.Id])
            {
                SkippedPipesDueToExistingIsoValves.Add(pipe);
                return;
            }

            if (pipe.Input.Diameter < PlacementOptions.PipeDiameterThreshold)
            {
                SkippedPipesDueToDiameterThreshold.Add(pipe);
                return;
            }

            if (pipe.Input.Length < PlacementOptions.PipeLengthThreshold)
            {
                SkippedPipesDueToLengthThreshold.Add(pipe);
                return;
            }

            if (pipe.Input.IsUserDefinedLength && PlacementOptions.SkipUserDefinedLength)
            {
                SkippedPipesDueToUserDefinedLength.Add(pipe);
                return;
            }

            // Find the locations on the pipe where the isolation value should be placed
            var locations = GetValvesLoations(pipe,
                PlacementOptions.IsolationValveDistanceFromEnd,
                PlacementOptions.ValveToValeDistance);

            // Add isolation valves
            foreach (var location in locations)
            {
                var valve = WaterModel.Network.IsolationValves.Create(
                    label: WaterModel.NextNetworkElementLabel(WaterNetworkElementType.IsolationValve),
                    point: location,
                    pipe: pipe);

                IsoValvesAdded.Add(valve);
            }

            return;
        }

        private List<GeometryPoint> GetValvesLoations(IPipe pipe, float endOffset, float atEvery)
        {
            var locations = new List<GeometryPoint>();
            var pipeLength = pipe.Input.Length;

            if (pipe.Input.Length < endOffset)
                return locations;

            var vertices = pipe.Input.GetPoints().ToArray();

            // first point location = endOffset distance on pipe
            var _ = 0;
            var firstPoint = MathLibrary.GetPointAtDistanceIntoPolyline(vertices, endOffset, out _);
            locations.Add(firstPoint);

            // last point and intermediate points
            var remaingLength = pipeLength - endOffset;
            if (remaingLength > endOffset)
            {

                var lastPoint = MathLibrary.GetPointAtDistanceIntoPolyline(vertices, remaingLength, out _);
                locations.Add(lastPoint);

                // intermediate points
                remaingLength = pipeLength - endOffset * 2;
                var valvesToCreate = (int)(remaingLength / atEvery);
                var newAtEvery = remaingLength / valvesToCreate; // evenly distribute the distance for the number of valves

                for (int i = 0; i < valvesToCreate; i++)
                {
                    var intermediatePoint = MathLibrary.GetPointAtDistanceIntoPolyline(vertices, endOffset + newAtEvery * (i + 1), out _);
                    locations.Add(intermediatePoint);
                }
            }

            return locations;
        }
        #endregion

        #region Public Properties
        public ValvePlacementOptionsControlModel ValvePlacementOptionsControlModel
            => valvePlacementOptionsControlModel
            ?? (valvePlacementOptionsControlModel = new ValvePlacementOptionsControlModel(ApplicationModel));
        public List<IIsolationValve> IsoValvesAdded { get; set; } = new List<IIsolationValve>();
        public List<IPipe> SkippedPipesDueToDiameterThreshold { get; set; } = new List<IPipe>();
        public List<IPipe> SkippedPipesDueToLengthThreshold { get; set; } = new List<IPipe>();
        public List<IPipe> SkippedPipesDueToExistingIsoValves { get; set; } = new List<IPipe>();
        public List<IPipe> SkippedPipesDueToUserDefinedLength { get; set; } = new List<IPipe>();
        #endregion

        #region Fields
        private ValvePlacementOptionsControlModel valvePlacementOptionsControlModel;
        #endregion


        #region Private Properties
        Dictionary<int, bool> PipeHasIsoValveMap { get; } = new Dictionary<int, bool>();
        ValvePlacementOptions PlacementOptions => ValvePlacementOptionsControlModel.PlacementOptions;
        WaterApplicationManager AppManager => (WaterApplicationManager)WaterApplicationManager.GetInstance();
        IWaterModel WaterModel => AppManager.CurrentWaterModel;
        #endregion
    }
}
