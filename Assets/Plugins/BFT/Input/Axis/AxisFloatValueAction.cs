using UnityEngine;
#if BFT_REWIRED
using Sirenix.OdinInspector;

namespace BFT
{
    public class AxisFloatValueAction : AxisAction
    {
#if UNITY_EDITOR

        public bool LogValue;

#endif

        [LabelText("Float Variable")] public FloatVariable Variable;

        public bool IsFloatGiverAssigned => Variable != null;

        public override void AxisValueAction(float value)
        {
            Variable.Value = value;
#if UNITY_EDITOR
            if (LogValue)
                UnityEngine.Debug.Log(value);
#endif
        }
    }
}

#endif
