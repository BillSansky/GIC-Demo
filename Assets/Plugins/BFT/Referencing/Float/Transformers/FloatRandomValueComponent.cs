using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
#endif

namespace BFT
{
    public class FloatRandomValueComponent : MonoBehaviour, IValue<float>
    {
        [SerializeField] private FloatValue from;
        [SerializeField] private FloatValue to;

        [ShowInInspector] public float Value => Random.Range(from.Value, to.Value);

        [Button]
        public void RefreshValue()
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
    }
}
