using UnityEngine;

namespace BFT
{
    public class BoolToFloatValueComponent : MonoBehaviour,
        IValue<float>
    {
        public BoolValue BoolValue;
        public FloatValue FalseValue;
        public FloatValue TrueValue;

        public float FloatValue => BoolValue.Value ? TrueValue.Value : FalseValue.Value;

        public float Value => FloatValue;
    }
}
