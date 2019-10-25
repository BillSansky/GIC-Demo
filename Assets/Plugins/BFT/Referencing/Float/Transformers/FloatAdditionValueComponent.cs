using UnityEngine;
using UnityEngine.Serialization;

namespace BFT
{
    public class FloatAdditionValueComponent : MonoBehaviour, IValue<float>
    {
        [FormerlySerializedAs("AddendValue")] public FloatValue AddEndValue;
        public FloatValue FloatValue;

        public float Value => FloatValue.Value + AddEndValue.Value;
    }
}
