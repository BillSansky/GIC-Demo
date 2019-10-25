using UnityEngine;
#if BFT_REWIRED
using Sirenix.OdinInspector;

namespace BFT
{
    public class AxisOnConditionAction : AxisAction
    {
        public AxisAction ActionOnTrue;

        [InlineEditor] public IValue<bool> Condition;

        public override void AxisValueAction(float value)
        {
            if (Condition.Value)
            {
                ActionOnTrue.AxisValueAction(value);
            }
        }
    }
}

#endif
