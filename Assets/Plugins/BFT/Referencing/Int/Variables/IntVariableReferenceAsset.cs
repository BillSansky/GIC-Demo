using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    [CreateAssetMenu(menuName = "BFT/Data/Variables/Int Variable Reference", fileName = "Int Variable Reference")]
    public class IntVariableReferenceAsset : SerializedScriptableObject, IValue<int>
    {
        public IValue<int> ValueReference;
        public int Value => ValueReference.Value;
    }
}
