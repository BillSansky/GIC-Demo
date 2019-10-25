using UnityEngine;

namespace BFT
{
    public class FloatMultiplicationValueComponent : MonoBehaviour, IValue<float>
    {
        public FloatValue FloatValue;
        public FloatValue MultiplierValue;

        public float Value => FloatValue.Value * MultiplierValue.Value;
    }
}
