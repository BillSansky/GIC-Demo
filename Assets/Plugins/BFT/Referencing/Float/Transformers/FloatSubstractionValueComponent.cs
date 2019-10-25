using UnityEngine;

namespace BFT
{
    public class FloatSubstractionValueComponent : MonoBehaviour, IValue<float>
    {
        public FloatValue FloatValue;
        public FloatValue MinuendValue;

        public float Value => FloatValue.Value - MinuendValue.Value;
    }
}
