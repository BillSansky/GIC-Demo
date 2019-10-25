using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    [AddComponentMenu("Referencing/Percent/Invert Percent")]
    public class InvertedPercentValue : SerializedMonoBehaviour, IValue<float>
    {
        public IValue<float> PercentValue;

        [ShowInInspector, ReadOnly] public float Value => 1 - PercentValue.Value;
    }
}
