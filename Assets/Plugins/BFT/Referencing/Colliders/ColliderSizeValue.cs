using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class ColliderSizeValue : SerializedMonoBehaviour, IValue<float>
    {
        public enum Axis
        {
            X,
            Y,
            Z
        }

        public Axis axis;

        public bool GetHalfValue = false;
        public Collider relatedCollider;

        public float Value
        {
            get
            {
                float size = 0;
                if (relatedCollider is SphereCollider)
                {
                    size = (relatedCollider as SphereCollider).radius * 2;
                }
                else
                {
                    switch (axis)
                    {
                        case Axis.X:
                            if (relatedCollider is BoxCollider)
                            {
                                size = (relatedCollider as BoxCollider).size.x;
                            }
                            else if (relatedCollider is CapsuleCollider)
                            {
                                size = (relatedCollider as CapsuleCollider).radius * 2;
                            }

                            break;
                        case Axis.Y:
                            if (relatedCollider is BoxCollider)
                            {
                                size = (relatedCollider as BoxCollider).size.y;
                            }
                            else if (relatedCollider is CapsuleCollider)
                            {
                                size = (relatedCollider as CapsuleCollider).height;
                            }

                            break;
                        case Axis.Z:
                            if (relatedCollider is BoxCollider)
                            {
                                size = (relatedCollider as BoxCollider).size.z;
                            }
                            else if (relatedCollider is CapsuleCollider)
                            {
                                size = (relatedCollider as CapsuleCollider).radius * 2;
                            }

                            break;
                    }
                }

                if (GetHalfValue)
                    size *= 0.5f;

                return size;
            }
        }
    }
}
