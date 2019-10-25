using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace BFT
{
    [CreateAssetMenu(menuName = "BFT/Data/Variables/Float Addition Variable", fileName = "Float Addition Variable")]
    public class FloatAdditionValueAsset : SerializedScriptableObject, IValue<float>
    {
        [FormerlySerializedAs("AddendValue")] public FloatValue AddEndValue;
        public FloatValue FloatValue;

        public float Value => FloatValue.Value + AddEndValue.Value;
    }
}
