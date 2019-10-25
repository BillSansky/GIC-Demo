using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace BFT
{
    public class BoundsTransformOffset : UpdateTypeSwitchable
    {
        public float AdditionalOffset = 0;

        private Vector3 boundsCenter;

        [FormerlySerializedAs("LocalSpacePivot")]
        public Vector3Value BoundsLocalSpacePivot;

        private Vector3 centerToPivot;
        private Vector3 direction;
        private Vector3 forward;

        [FormerlySerializedAs("OffsetDistances")]
        public BoundsValue OffsetBounds;

        [InfoBox("This component offsets the position of the transform in a given direction with a given distance")]
        public Vector3Value OffsetDirection;

        public float OffsetMultiplier = 1f;

        [HideIf("UseGlobalOrientation")] public OrientationValue OffsetOrientation;
        private Vector3 offsets;
        private Vector3 pivot;

        private float previousOffset = 0;

        public UnityEngine.Transform ReferenceTransform;
        private Vector3 right;
        public float SmoothingSpeed = 1;

        public bool SmoothOffsetValueTransition = true;

        private Vector3 up;

        [SerializeField] private bool useGlobalOrientation = false;

        public bool UseGlobalOrientation
        {
            get => useGlobalOrientation;
            set => useGlobalOrientation = value;
        }

        public void SetOffsetDirection(PropertyComponent offset)
        {
            OffsetDirection.SetAndUseReference(offset);
        }

        public void SetOffsetBounds(PropertyComponent distance)
        {
            UnityEngine.Debug.Assert(distance.Data is IValue<Bounds>,
                $"The distance object {distance.name} was not of the expected type", this);

            OffsetBounds.SetAndUseReference(distance);
        }

        public void SetBoundsLocalPivot(PropertyComponent pivot)
        {
            BoundsLocalSpacePivot.SetAndUseReference(pivot);
        }

        public void SetOffsetOrientation(PropertyComponent orientation)
        {
            OffsetOrientation.SetAndUseReference(orientation);
        }

        public override void UpdateMethod()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                if (OffsetDirection == null || OffsetBounds == null || !ReferenceTransform ||
                    OffsetOrientation == null)
                {
                    return;
                }
            }
#endif

            boundsCenter = OffsetBounds.Value.center;
            pivot = BoundsLocalSpacePivot.Value;
            offsets = OffsetBounds.Value.extents;
            direction = OffsetDirection.Value;

            centerToPivot = boundsCenter - pivot;

            up = useGlobalOrientation ? Vector3.up : OffsetOrientation.Value.Up;
            right = useGlobalOrientation ? Vector3.right : OffsetOrientation.Value.Right;
            forward = useGlobalOrientation ? Vector3.forward : OffsetOrientation.Value.Forward;

            float dotRight = Vector3.Dot(right, direction);
            float dotUp = Vector3.Dot(up, direction);
            float dotForward = Vector3.Dot(forward, direction);


            float xOffset = offsets.x * Mathf.Abs(dotRight) + centerToPivot.x * dotRight;
            float yOffset = offsets.y * Mathf.Abs(dotUp) + centerToPivot.y * dotUp;
            float zOffset = offsets.z * Mathf.Abs(dotForward) + centerToPivot.z * dotForward;

            float offset = xOffset + yOffset + zOffset;

            if (SmoothOffsetValueTransition)
            {
                offset = Mathf.Lerp(previousOffset, offset, UnityEngine.Time.deltaTime * SmoothingSpeed);
                previousOffset = offset;
            }

            transform.position = ReferenceTransform.position + (offset * OffsetMultiplier + AdditionalOffset) * direction;
        }

        public void OnDrawGizmosSelected()
        {
            if (!ReferenceTransform)
                return;

            float dotRight = Vector3.Dot(right, direction);
            float dotUp = Vector3.Dot(up, direction);
            float dotForward = Vector3.Dot(forward, direction);


            float xOffset = offsets.x * Mathf.Abs(dotRight) + centerToPivot.x * dotRight;
            float yOffset = offsets.y * Mathf.Abs(dotUp) + centerToPivot.y * dotUp;
            float zOffset = offsets.z * Mathf.Abs(dotForward) + centerToPivot.z * dotForward;

            Gizmos.color = Color.red;
            var position = ReferenceTransform.position;
            Gizmos.DrawLine(position,
                position + (xOffset * OffsetMultiplier + AdditionalOffset) * right);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(position,
                position + (yOffset * OffsetMultiplier + AdditionalOffset) * up);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(position,
                position + (zOffset * OffsetMultiplier + AdditionalOffset) * forward);

            float offset = xOffset + yOffset + zOffset;

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(position,
                position + offset * OffsetMultiplier * direction);
        }
    }
}
