using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    /// <summary>
    ///     Set a transform reference's value from a transform
    /// </summary>
    public class TransformReferenceSetter : MonoBehaviour
    {
        [BoxGroup("Options")] public bool CopyOnAwake = true;

        [BoxGroup("Options")] public bool CopyOnEnable = false;

        public UnityEvent OnTransformSet;
        [BoxGroup("Transforms")] public TransformVariable TransformReference;

        [BoxGroup("Transforms")] public UnityEngine.Transform TransformToCopyOnReference;

        public void Reset()
        {
            TransformToCopyOnReference = transform;
        }

        public void Awake()
        {
            if (CopyOnAwake)
                CopyTransformReference();
        }

        public void OnEnable()
        {
            if (CopyOnEnable)
                CopyTransformReference();
        }

        public void CopyTransformReference()
        {
            TransformReference.Value = TransformToCopyOnReference;
            OnTransformSet.Invoke();
        }
    }
}
