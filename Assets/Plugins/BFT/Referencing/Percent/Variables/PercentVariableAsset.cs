using UnityEngine;

namespace BFT
{
    public class PercentVariableAsset : VariableAsset<float>
    {
        protected override void ChangeCachedValue()
        {
            Value = Mathf.Clamp01(Value);
            base.ChangeCachedValue();
        }
    }
}
