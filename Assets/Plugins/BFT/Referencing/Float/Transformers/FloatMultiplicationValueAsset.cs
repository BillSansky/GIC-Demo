using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    [CreateAssetMenu(menuName = "BFT/Data/Variables/Float Multiplication Variable",
        fileName = "Float Multiplication Variable")]
    public class FloatMultiplicationValueAsset : SerializedScriptableObject, IValue<float>
    {
        public IValue<float> FloatValue;
        public IValue<float> MultiplierValue;

        public float Value => FloatValue.Value * MultiplierValue.Value;
    }
}
