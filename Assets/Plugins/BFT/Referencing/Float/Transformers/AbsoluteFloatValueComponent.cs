using UnityEngine;

namespace BFT
{
    public class AbsoluteFloatValueComponent : MonoBehaviour, IValue<float>
    {
        public FloatValue InputValue;
        public float Value => Mathf.Abs(InputValue.Value);
    }
}
