using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    [Serializable]
    public class PercentValue : GenericValue<float>
    {
        public PercentValue()
        {
        }

        public PercentValue(float f)
        {
            LocalValue = f;
        }

        [HorizontalGroup]
        [HideIf("UseReference"), HideLabel, ShowInInspector]
        [PropertyRange(0, 1)]
        protected override float LocalValueInspected
        {
            get => LocalValue;
            set => LocalValue = value;
        }

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
