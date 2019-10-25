using System;
using UnityEngine;

namespace BFT
{
    [Serializable]
    public class PercentVariable : GenericVariable<float>
    {
        public new float Value
        {
            get => Mathf.Clamp01(base.Value);
            set
            {
                value = Mathf.Clamp01(value);
                if (UseReference)
                    LocalValue = value;
                else
                    ((IVariable<float>) Reference).Value = value;
            }
        }

        protected override void CheckType()
        {
            base.CheckType();
            Value = Mathf.Clamp01(Value);
        }
    }
}
