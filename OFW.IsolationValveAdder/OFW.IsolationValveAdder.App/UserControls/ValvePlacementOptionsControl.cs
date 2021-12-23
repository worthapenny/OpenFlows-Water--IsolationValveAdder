/**
 * @ Author: Akshaya Niraula
 * @ Create Time: 2021-12-22 4:42
 * @ Copyright: Copyright (c) 2021 Akshaya Niraula. See LICENSE for details
 */

using Haestad.Framework.Windows.Forms.Components;
using OFW.IsolationValveAdder.App.UserControlModel;
using OpenFlows.Water.Application;
using System.Linq;
using System.Windows.Forms;

namespace OFW.IsolationValveAdder.App.UserControls
{
    public partial class ValvePlacementOptionsControl : HaestadUserControl
    {
        #region Constructor
        public ValvePlacementOptionsControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Override Methods
        protected override void InitializeBindings()
        {
            // Diameter Threshold
            this.trackBarDiameterThreshold.DataBindings.Add(
                nameof(trackBarDiameterThreshold.Value),
                PlacementOptionsControlModel.PlacementOptions,
                nameof(PlacementOptionsControlModel.PlacementOptions.PipeDiameterThreshold),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.labelDiameterThresholdValue.DataBindings.Add(
                nameof(this.labelDiameterThresholdValue.Text),
                PlacementOptionsControlModel.PlacementOptions,
                nameof(PlacementOptionsControlModel.PlacementOptions.PipeDiameterThreshold),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            // Length Threshold
            this.trackBarLengthThreshold.DataBindings.Add(
                nameof(trackBarLengthThreshold.Value),
                PlacementOptionsControlModel.PlacementOptions,
                nameof(PlacementOptionsControlModel.PlacementOptions.PipeLengthThreshold),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.labelLengthThresholdValue.DataBindings.Add(
                nameof(this.labelLengthThresholdValue.Text),
                PlacementOptionsControlModel.PlacementOptions,
                nameof(PlacementOptionsControlModel.PlacementOptions.PipeLengthThreshold),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            // Distance From End
            this.trackBarValveDistanceFromEnd.DataBindings.Add(
                nameof(this.trackBarValveDistanceFromEnd.Value),
                PlacementOptionsControlModel.PlacementOptions,
                nameof(PlacementOptionsControlModel.PlacementOptions.IsolationValveDistanceFromEnd),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.labelValveDistanceFromEndValue.DataBindings.Add(
                nameof(this.labelValveDistanceFromEndValue.Text),
                PlacementOptionsControlModel.PlacementOptions,
                nameof(PlacementOptionsControlModel.PlacementOptions.IsolationValveDistanceFromEnd),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            // Density (Isolation Valve at Every)
            this.trackBarIsoValveAtEvery.DataBindings.Add(
                nameof(this.trackBarIsoValveAtEvery.Value),
                PlacementOptionsControlModel.PlacementOptions,
                nameof(PlacementOptionsControlModel.PlacementOptions.ValveToValeDistance),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.labelValveAtEveryValue.DataBindings.Add(
                nameof(this.labelValveAtEveryValue.Text),
                PlacementOptionsControlModel.PlacementOptions,
                nameof(PlacementOptionsControlModel.PlacementOptions.ValveToValeDistance),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            // Skip pipes with User-Defined Length
            this.checkBoxSkipPipeWithUserDefinedLength.DataBindings.Add(
                nameof(this.checkBoxSkipPipeWithUserDefinedLength.Checked),
                PlacementOptionsControlModel.PlacementOptions,
                nameof(PlacementOptionsControlModel.PlacementOptions.SkipUserDefinedLength),
                false,
                DataSourceUpdateMode.OnPropertyChanged);
        }
        protected override void InitializeVisually()
        {
            var diametersMax = AppManager.CurrentWaterModel.Network.Pipes.Input.Diameters().Values.Max();
            var lengthsMax = AppManager.CurrentWaterModel.Network.Pipes.Input.Lengths().Values.Max();

            var diameterTickFrequency = (int)(diametersMax / 20);
            this.trackBarDiameterThreshold.Minimum = 0;
            this.trackBarDiameterThreshold.Maximum = (int)(diametersMax);
            this.trackBarDiameterThreshold.TickFrequency = diameterTickFrequency == 0 ? 1 : diameterTickFrequency;
            this.trackBarDiameterThreshold.Value = this.trackBarDiameterThreshold.Minimum + this.trackBarDiameterThreshold.TickFrequency;

            this.trackBarIsoValveAtEvery.Minimum = 10;
            this.trackBarIsoValveAtEvery.Maximum = (int)(lengthsMax/3);
            this.trackBarIsoValveAtEvery.TickFrequency = (int)((this.trackBarIsoValveAtEvery.Maximum - this.trackBarIsoValveAtEvery.Minimum) / 20);
            this.trackBarIsoValveAtEvery.Value = this.trackBarIsoValveAtEvery.Minimum + this.trackBarIsoValveAtEvery.TickFrequency;

            this.trackBarLengthThreshold.Minimum = 0;
            this.trackBarLengthThreshold.Maximum = (int)(lengthsMax/3);
            this.trackBarLengthThreshold.TickFrequency = (int)((this.trackBarLengthThreshold.Maximum - this.trackBarLengthThreshold.Minimum) / 20);
            this.trackBarLengthThreshold.Value = this.trackBarLengthThreshold.Minimum + this.trackBarLengthThreshold.TickFrequency;

            this.trackBarValveDistanceFromEnd.Minimum = 5;
            this.trackBarValveDistanceFromEnd.Maximum = (int)(lengthsMax/5);
            this.trackBarValveDistanceFromEnd.TickFrequency = (int)((this.trackBarValveDistanceFromEnd.Maximum - this.trackBarValveDistanceFromEnd.Minimum) / 10);
            this.trackBarValveDistanceFromEnd.Value = this.trackBarValveDistanceFromEnd.Minimum + this.trackBarValveDistanceFromEnd.TickFrequency;
        }
        protected override void InitializeText()
        {
            var lengthUnit = AppManager.CurrentWaterModel.Units.NetworkUnits.Pipe.LengthUnit.ShortLabel;
            var diameterUnit = AppManager.CurrentWaterModel.Units.NetworkUnits.Pipe.DiameterUnit.ShortLabel;

            this.labelDiameterThreshold.Text += $" ({diameterUnit})";
            this.labelDistanceFromEnd.Text += $" ({lengthUnit})";
            this.labelIsoValveAtEvery.Text += $" ({lengthUnit})";
            this.labelLengthThreshold.Text += $" ({lengthUnit})";
        }
        #endregion

        #region Private Properties
        ValvePlacementOptionsControlModel PlacementOptionsControlModel => (ValvePlacementOptionsControlModel)UserControlModel;
        WaterApplicationManager AppManager => (WaterApplicationManager)WaterApplicationManager.GetInstance();

        #endregion

        #region Public Properties
        // Do NOT expose the ControlModel from the UserControl
        //public ValvePlacementOptionsControlModel PlacementOptionsControlModel { get; set; }
        #endregion
    }
}
