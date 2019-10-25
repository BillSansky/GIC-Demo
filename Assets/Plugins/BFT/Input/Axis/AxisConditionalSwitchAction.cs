#if BFT_REWIRED

using UnityEngine;

namespace BFT
{
    public class AxisConditionalSwitchAction : AxisAction
    {
        public AxisAction ActionOnFalse;

        public AxisAction ActionOnTrue;

        public IValue<bool> Condition;

        public override void AxisValueAction(float value)
        {
            if (Condition.Value)
            {
                ActionOnTrue.AxisValueAction(value);
            }
            else
            {
                ActionOnFalse.AxisValueAction(value);
            }
        }
    }
}

#endif
