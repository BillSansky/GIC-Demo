using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class DirectionalTransformOffset : UpdateTypeSwitchable
    {
        [InfoBox("This component offsets the position of the transform in a given direction with a given distance")]
        public Vector3Variable OffsetDirection;

        public FloatValue OffsetDistance;

        public float OffsetMultiplier = 1f;

        public UnityEngine.Transform ReferenceTransform;

        public override void UpdateMethod()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                if (OffsetDirection == null || OffsetDistance == null || !ReferenceTransform)
                {
                    return;
                }
            }
#endif
            transform.position =
                ReferenceTransform.position + OffsetDistance.Value * OffsetMultiplier * OffsetDirection.Value;
        }
    }
}
