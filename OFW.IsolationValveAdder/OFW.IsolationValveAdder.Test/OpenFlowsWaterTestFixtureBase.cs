/**
 * @ Author: Akshaya Niraula
 * @ Create Time: 2021-12-22 4:42
 * @ Copyright: Copyright (c) 2021 Akshaya Niraula. See LICENSE for details
 */

using Haestad.Framework.Application;
using NUnit.Framework;
using OpenFlows.Application;
using OpenFlows.Water.Application;
using OpenFlows.Water.Domain;
using System.IO;

namespace OFW.IsolationValveAdder.Test
{
    public abstract class OpenFlowsWaterTestFixtureBase
    {
        #region Constructor
        public OpenFlowsWaterTestFixtureBase()
        {

        }
        #endregion

        #region Setup/Tear-down
        [SetUp]
        public void Setup()
        {
            ApplicationManagerBase.SetApplicationManager(new WaterApplicationManager());

            // By passing in false, this will suppress the primary user interface.
            // Make sure you are logged into CONNECTION client.
            WaterApplicationManager.GetInstance().Start(false);

            SetupImpl();
        }
        protected virtual void SetupImpl()
        {
        }
        [TearDown]
        public void Teardown()
        {
            TeardownImpl();

            WaterApplicationManager.GetInstance().Stop();
        }
        protected virtual void TeardownImpl()
        {

        }
        #endregion

        #region Protected Methods
        protected void OpenModel(string filename)
        {
            ProjectProperties app = ProjectProperties.Default;
            app.NominalProjectPath = filename;
            WaterApplicationManager.GetInstance().ParentFormModel.OpenProject(app);
        }
        protected virtual string BuildTestFilename(string filename)
        {
            // The default base path is the samples folder for WaterGEMS. You can change this to
            // whatever you want.  Remember this is the BASE path as it will be combined with the provided filename.
            return Path.Combine(@"C:\Program Files (x86)\Bentley\WaterGEMS\Samples", filename);
        }
        #endregion

        #region Protected Properties
        protected WaterApplicationManager AppManager => (WaterApplicationManager)WaterApplicationManager.GetInstance();
        protected IWaterModel WaterModel => AppManager.CurrentWaterModel;
        protected IProject Project => AppManager.ParentFormModel.CurrentProject;
        #endregion
    }
}
