#if BFT_REWIRED
using System.Collections.Generic;
using UnityEngine;

namespace BFT
{
    public class AxisActionList : AxisAction
    {
        public List<AxisAction> actions;

        public override void AxisValueAction(float value)
        {
            foreach (AxisAction axisAction in actions)
            {
                axisAction.AxisValueAction(value);
            }
        }
    }
}
#endif
