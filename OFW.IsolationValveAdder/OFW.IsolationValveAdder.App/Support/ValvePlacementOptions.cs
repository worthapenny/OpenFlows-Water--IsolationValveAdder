/**
 * @ Author: Akshaya Niraula
 * @ Create Time: 2021-12-22 4:42
 * @ Copyright: Copyright (c) 2021 Akshaya Niraula. See LICENSE for details
 */

namespace OFW.IsolationValveAdder.App.Support
{
    public class ValvePlacementOptions
    {
        #region Constructor
        public ValvePlacementOptions()
        {
        }
        #endregion

        #region Public Properties
        public int ScenarioId { get; set; }
        public int ValveToValeDistance { get; set; }
        public float IsolationValveDistanceFromEnd { get; set; }
        public float PipeLengthThreshold { get; set; }
        public float PipeDiameterThreshold { get; set; }
        public bool SkipUserDefinedLength { get; set; } = true;
        #endregion
    }
}
