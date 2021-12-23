/**
 * @ Author: Akshaya Niraula
 * @ Create Time: 2021-12-22 4:42
 * @ Copyright: Copyright (c) 2021 Akshaya Niraula. See LICENSE for details
 */

using Haestad.Drawing.Domain;
using Haestad.Framework.Application;
using Haestad.LicensingFacade;
using Haestad.WaterProduct.Application;
using Haestad.WaterProduct.Drawing;
using OFW.IsolationValveAdder.App.Forms;
using OpenFlows.Application;
using OpenFlows.Water.Application;
using System;

namespace OFW.IsolationValveAdder.App
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static int Main()
        {
            WaterApplicationManager.SetApplicationManager(new WaterAppManager());
            WaterApplicationManager.GetInstance().SetParentFormSurrogateDelegate(
                new ParentFormSurrogateDelegate((fm) =>
                {
                    return new ValveAdderParentForm(fm);
                }));

            // Start the session
            WaterApplicationManager.GetInstance().Start(true);

            // End the session
            WaterApplicationManager.GetInstance().Stop();

            return 0;
        }
    }

    public class WaterAppManager : WaterApplicationManager
    {
        protected override bool IsHeadless => false;

        protected override IDomainApplicationModel NewApplicationModel()
            => new WaterProductApplicationModel(LicensePlatformType.Standalone, "10.00.00.00", null);

        //
        // This is a temporary fix and will not need in the future (until the layoutController type is not ArcMap)
        protected override HaestadParentFormModel NewParentFormModel()
        {
            return new WaterParentFormModelEx(WaterApplicationModel);
        }
    }

    //
    // This is a temporary fix and will not need in the future (until the layoutController type is not ArcMap)
    public class WaterParentFormModelEx : WaterParentFormModel
    {
        public WaterParentFormModelEx(IApplicationModel aapplicationModel) : base(aapplicationModel)
        {            
        }

        protected override LayoutControllerBase NewLayoutController()
        {
            LayoutControllerBase layoutController = new WaterProductLayoutController(DomainApplicationModel, ParentFormUIModel);
            layoutController.FeatureManager = new WaterProductFeatureManager();
            layoutController.BatchFeatureManager = new StandAloneBatchFeatureManager(layoutController.FeatureManager);
            return layoutController;
        }
    }
}
