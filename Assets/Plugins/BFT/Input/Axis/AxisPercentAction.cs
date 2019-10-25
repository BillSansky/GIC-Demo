#if BFT_REWIRED
using UnityEngine;

namespace BFT
{
    public class AxisPercentAction : AxisAction
    {
        public float MaxPercent = 1;

        public float MinPercent = 0;
        public PercentVariable Percent;

        public override void AxisValueAction(float value)
        {
            Percent.Value = Mathf.Clamp(value, MinPercent, MaxPercent);
        }
    }
}

#endif
