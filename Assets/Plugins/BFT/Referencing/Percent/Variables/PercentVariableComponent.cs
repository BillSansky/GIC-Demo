using UnityEngine;

namespace BFT
{
    public class PercentVariableComponent : VariableComponent<float>
    {
        public PercentVariable PercentVariable;
        public override GenericVariable<float> Variable => PercentVariable;

        public new float Value
        {
            get => Mathf.Clamp01(base.Value);
            set
            {
                value = Mathf.Clamp01(value);
                base.Value = value;
            }
        }
    }
}
