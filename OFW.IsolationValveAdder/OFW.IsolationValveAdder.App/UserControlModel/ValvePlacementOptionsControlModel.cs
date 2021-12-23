/**
 * @ Author: Akshaya Niraula
 * @ Create Time: 2021-12-22 4:42
 * @ Copyright: Copyright (c) 2021 Akshaya Niraula. See LICENSE for details
 */

using Haestad.Framework.Application;
using OFW.IsolationValveAdder.App.Support;

namespace OFW.IsolationValveAdder.App.UserControlModel
{
    public class ValvePlacementOptionsControlModel : HaestadUserControlModel
    {
        #region Constructor
        public ValvePlacementOptionsControlModel(IApplicationModel appModel)
            :base("ValvePlacementOptionsControlModel", appModel)
        {
        }
        #endregion

        #region Public Properties
        public ValvePlacementOptions PlacementOptions { get;} = new ValvePlacementOptions();
        #endregion
    }
}
