/**
 * @ Author: Akshaya Niraula
 * @ Create Time: 2021-12-22 4:42
 * @ Copyright: Copyright (c) 2021 Akshaya Niraula. See LICENSE for details
 */

using Haestad.Framework.Application;
using Haestad.Support.User;
using NUnit.Framework;
using OFW.IsolationValveAdder.App.FormModel;
using OpenFlows.Water.Application;

namespace OFW.IsolationValveAdder.Test
{
    [TestFixture]
    public class TestValveAdder : OpenFlowsWaterTestFixtureBase
    {
        #region Setup/Teardown
        protected override void SetupImpl()
        {
            var fullPath = BuildTestFilename("Example5.wtg");
            OpenModel(fullPath);
        }
        protected override void TeardownImpl()
        {
            ProjectProperties app = ProjectProperties.Default;

            // Calling MakeClean() will suppress any prompts to save and treat the project as if no changes were made.
            ((ProjectBase)WaterApplicationManager.GetInstance().ParentFormModel.CurrentProject).MakeClean();
            WaterApplicationManager.GetInstance().ParentFormModel.CloseCurrentProject(app);
        }
        #endregion

        #region Tests
        [Test]
        public void TestExample1()
        {
            // make sure there are not any isolation valve
            Assert.AreEqual(WaterModel.Network.IsolationValves.Count, 0);

            var valveAdderFormModel = new ValveAdderFormModel(AppManager.WaterApplicationModel);
            var options = valveAdderFormModel.ValvePlacementOptionsControlModel.PlacementOptions;

            options.ScenarioId = WaterModel.ActiveScenario.Id;
            options.PipeLengthThreshold = 150;
            options.PipeDiameterThreshold = 300;
            options.ValveToValeDistance = 100;
            options.IsolationValveDistanceFromEnd = 10;


            valveAdderFormModel.AddIsolationValves(new NullProgressIndicator());

            // make sure the isolation valves are created now
            Assert.AreEqual(WaterModel.Network.IsolationValves.Count, 66);
            Assert.AreEqual(valveAdderFormModel.SkippedPipesDueToDiameterThreshold.Count, 247);
            Assert.AreEqual(valveAdderFormModel.SkippedPipesDueToLengthThreshold.Count, 6);
            Assert.AreEqual(valveAdderFormModel.SkippedPipesDueToExistingIsoValves.Count, 0);


            //AppManager.ParentFormModel.SaveAsProject(
            //    new ProjectProperties()
            //    {
            //        NominalProjectPath = @"D:\temp\isoVavlves_test.wtg"
            //    }) ;
        }
        #endregion
    }
}
