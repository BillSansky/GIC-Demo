using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class ColliderCenterValue : SerializedMonoBehaviour, IValue<float>
    {
        public enum Axis
        {
            X,
            Y,
            Z
        }

        public Axis axis;
        public Collider relatedCollider;

        public float Value
        {
            get
            {
                Vector3 center = Vector3.zero;

                if (relatedCollider is BoxCollider)
                {
                    center = (relatedCollider as BoxCollider).center;
                }
                else if (relatedCollider is CapsuleCollider)
                {
                    center = (relatedCollider as CapsuleCollider).center;
                }
                else if (relatedCollider is SphereCollider)
                {
                    center = (relatedCollider as SphereCollider).center;
                }

                switch (axis)
                {
                    case Axis.X:
                        return center.x;
                    case Axis.Y:
                        return center.y;
                    case Axis.Z:
                        return center.z;
                }

                return 0;
            }
        }
    }
}
