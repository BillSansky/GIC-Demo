using UnityEngine;

namespace BFT
{
    /// <summary>
    ///     Gives accces to certain transform operations for delegates and events
    /// </summary>
    public class TransformTools : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Transform targetTransform;

        public UnityEngine.Transform Transform
        {
            get
            {
                if (targetTransform == null)
                    return transform;
                return targetTransform;
            }
            set => targetTransform = value;
        }

        public Vector3 LocalPosition => Transform.localPosition;

        public Vector3 Position => Transform.position;

        public Vector3 LocalScale => Transform.localScale;
        public float LocalScaleX => Transform.localScale.x;
        public float LocalScaleY => Transform.localScale.y;
        public float LocalScaleZ => Transform.localScale.z;

        public float LocalRotationX => Transform.localEulerAngles.x;
        public float LocalRotationY => Transform.localEulerAngles.y;
        public float LocalRotationZ => Transform.localEulerAngles.z;

        public void Reset()
        {
            targetTransform = transform;
        }

        public void ResetLocalPosition()
        {
            Transform.localPosition = Vector3.zero;
        }

        public void ResetLocalRotation()
        {
            Transform.localRotation = Quaternion.identity;
        }

        public void ResetLocalScale()
        {
            Transform.localScale = Vector3.one;
        }

        public void ResetAllLocal()
        {
            ResetLocalPosition();
            ResetLocalRotation();
            ResetLocalScale();
        }

        public void SetLocalPosition(Vector3 position)
        {
            Transform.localPosition = position;
        }

        public void SetPosition(Vector3 position)
        {
            Transform.position = position;
        }

        public void SetLocalScale(Vector3 scale)
        {
            Transform.localScale = scale;
        }

        public void SetLocalScale(float unifiedScale)
        {
            Transform.localScale = Vector3.one * unifiedScale;
        }

        public void SetLocalScaleX(float scale)
        {
            Transform.localScale = new Vector3(scale, Transform.localScale.y, Transform.localScale.z);
        }

        public void SetLocalScaleY(float scale)
        {
            Transform.localScale = new Vector3(Transform.localScale.x, scale, Transform.localScale.z);
        }

        public void SetLocalScaleZ(float scale)
        {
            Transform.localScale = new Vector3(Transform.localScale.x, Transform.localScale.y, scale);
        }

        public void SetLocalRotationX(float rotation)
        {
            Transform.localRotation = Quaternion.Euler(rotation, Transform.localRotation.y, Transform.localRotation.z);
        }

        public void SetLocalRotationY(float rotation)
        {
            Transform.localRotation = Quaternion.Euler(Transform.localRotation.x, rotation, Transform.localRotation.z);
        }

        public void SetLocalRotationZ(float rotation)
        {
            Transform.localRotation = Quaternion.Euler(Transform.localRotation.x, Transform.localRotation.y, rotation);
        }

        public void CopyTransformPosition(UnityEngine.Transform other)
        {
            Transform.Copy(other, true, false, false);
        }
    }
}
