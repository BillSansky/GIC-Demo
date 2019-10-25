using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class TransformScaleValue : SerializedMonoBehaviour, IValue<float>
    {
        public enum Axis
        {
            X,
            Y,
            Z
        }

        [SerializeField] private Axis axis;

        public bool LocalScale;

        public UnityEngine.Transform RelatedTransform;

        public float Value
        {
            get
            {
                Vector3 scale = LocalScale ? RelatedTransform.localScale : RelatedTransform.lossyScale;
                switch (axis)
                {
                    case Axis.X:
                        return scale.x;
                    case Axis.Y:
                        return scale.y;
                    case Axis.Z:
                        return scale.z;
                }

                return 0;
            }
        }
    }
}
