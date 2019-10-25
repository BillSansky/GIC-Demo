using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    public enum ColliderPropertyType
    {
        BOX,
        SPHERE,
        CAPSULE,
    }

    public class ColliderPropertiesEvent : UnityEvent<ColliderProperties>
    {
    }

    [Serializable]
    public class ColliderProperties : IBoxProperties, ICapsuleProperties, ISphereProperties
    {
        [BoxGroup("Position")] [SerializeField]
        private Vector3 center;

        [EnumToggleButtons] public ColliderPropertyType ColliderType;


        [BoxGroup("Properties")] [ShowIf("IsBox")] [SerializeField]
        private Vector3 halfSize;

        [BoxGroup("Properties")] [ShowIf("IsCapsule")] [SerializeField]
        private float height;


        [BoxGroup("Properties")] [ShowIf("IsCapsuleOrSphere")] [SerializeField]
        private float radius;

        public bool IsCapsule => ColliderType == ColliderPropertyType.CAPSULE;
        public bool IsBox => ColliderType == ColliderPropertyType.BOX;
        public bool IsSphere => ColliderType == ColliderPropertyType.SPHERE;

        public bool IsCapsuleOrSphere => IsCapsule || IsSphere;


        public float SphereRadius => ((ISphereProperties) this).Radius;
        public float CapsuleRadius => ((ICapsuleProperties) this).Radius;

        //The radius independently of the collider type
        public float GeneralRadius
        {
            get
            {
                switch (ColliderType)
                {
                    case ColliderPropertyType.BOX:
                        return HalfSize.x * 2;
                    case ColliderPropertyType.SPHERE:
                        return SphereRadius;
                    case ColliderPropertyType.CAPSULE:
                        return CapsuleRadius;
                    default:
                        return 0;
                }
            }
        }

        public Vector3 HalfSize
        {
            get => halfSize;
            set => halfSize = value;
        }


        public Vector3 Center
        {
            get => center;
            set => center = value;
        }

        public float Height
        {
            get => height;
            set => height = value;
        }

        public float Radius
        {
            get => radius;
            set => radius = value;
        }
    }
}
