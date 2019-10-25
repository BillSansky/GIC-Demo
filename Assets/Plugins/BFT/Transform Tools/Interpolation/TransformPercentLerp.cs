using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    [ExecuteInEditMode]
    public class TransformPercentLerp : SerializedMonoBehaviour
    {
        public PercentValue Percent;

        public TransformValue TransformA;
        public TransformValue TransformB;

        public void Update()
        {
            if (Percent != null)
                transform.LerpPositionRotation(TransformA.Value, TransformB.Value, Percent.Value);
#if UNITY_EDITOR
            if (!Application.isPlaying)
                transform.LerpPositionRotation(TransformA.Value, TransformB.Value, DebugRange);
#endif
        }


        [Button]
        public void MoveToTransformA()
        {
            transform.LerpPositionRotation(TransformA.Value, TransformB.Value, 0);
#if UNITY_EDITOR
            DebugRange = 0;
#endif
        }

        [Button]
        public void MoveToTransformB()
        {
            transform.LerpPositionRotation(TransformA.Value, TransformB.Value, 1);
#if UNITY_EDITOR
            DebugRange = 1;
#endif
        }

        [Button]
        public void CopyTransformIntoA()
        {
            TransformA.position = transform.position;
            TransformA.rotation = transform.rotation;
        }

        [Button]
        public void CopyTransformIntoB()
        {
            TransformB.position = transform.position;
            TransformB.rotation = transform.rotation;
        }

#if UNITY_EDITOR

        public bool DebugInEditor;

        [Range(0, 1)] public float DebugRange;

#endif
    }
}
