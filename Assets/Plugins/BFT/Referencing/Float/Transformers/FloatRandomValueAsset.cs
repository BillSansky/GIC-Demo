using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    [CreateAssetMenu(menuName = "BFT/Data/Variables/Float Random", fileName = "Float Random")]
    public class FloatRandomValueAsset : SerializedScriptableObject, IValue<float>
    {
        [SerializeField] private IValue<float> from;

        [SerializeField] private IValue<float> to;

        public float Value => Random.Range(from.Value, to.Value);
    }
}
