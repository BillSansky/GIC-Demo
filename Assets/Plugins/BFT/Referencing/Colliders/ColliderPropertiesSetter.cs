using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class ColliderPropertiesSetter : SerializedMonoBehaviour, IValue<ColliderProperties>
    {
        [OnValueChanged("CreateAppropriateColliderAndSetValues")]
        public IValue<ColliderProperties> ColliderProperties;

        private Collider currentCollider;

        public bool ErasePreviousColliderOnChange = true;
        public bool Trigger;

        public ColliderProperties Value => ColliderProperties.Value;

        void Awake()
        {
            CreateAppropriateColliderAndSetValues();
        }

        [OnInspectorGUI]
        private void OnGUICheckValues()
        {
            if (!Application.isPlaying)
                CreateAppropriateColliderAndSetValues();
        }

        [Button("Update Values", ButtonSizes.Medium)]
        private void CreateAppropriateColliderAndSetValues()
        {
            if (ColliderProperties == null)
                return;

            switch (ColliderProperties.Value.ColliderType)
            {
                case ColliderPropertyType.BOX:
                    currentCollider = gameObject.GetComponent<BoxCollider>();

                    ErasePreviousColliderIfNeeded();

                    if (!currentCollider)
                    {
                        currentCollider = gameObject.AddComponent<BoxCollider>();
                    }

                    BoxCollider col = (BoxCollider) currentCollider;
                    col.center = ColliderProperties.Value.Center;
                    col.size = ColliderProperties.Value.HalfSize * 2;

                    break;
                case ColliderPropertyType.SPHERE:
                    currentCollider = gameObject.GetComponent<SphereCollider>();

                    ErasePreviousColliderIfNeeded();

                    if (!currentCollider)
                    {
                        currentCollider = gameObject.AddComponent<SphereCollider>();
                    }

                    SphereCollider sph = (SphereCollider) currentCollider;
                    sph.radius = ColliderProperties.Value.SphereRadius;
                    sph.center = ColliderProperties.Value.Center;

                    break;
                case ColliderPropertyType.CAPSULE:
                    currentCollider = gameObject.GetComponent<CapsuleCollider>();

                    ErasePreviousColliderIfNeeded();

                    if (!currentCollider)
                    {
                        currentCollider = gameObject.AddComponent<CapsuleCollider>();
                    }

                    CapsuleCollider caps = (CapsuleCollider) currentCollider;
                    caps.center = ColliderProperties.Value.Center;
                    caps.height = ColliderProperties.Value.Height;
                    caps.radius = ColliderProperties.Value.CapsuleRadius;

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            currentCollider.isTrigger = Trigger;
        }

        private void ErasePreviousColliderIfNeeded()
        {
            if (!ErasePreviousColliderOnChange || !currentCollider)
            {
                currentCollider = gameObject.GetComponent<Collider>();
                if (currentCollider)
                    Destroy(currentCollider);
            }
        }

#if UNITY_EDITOR

        void OnDrawGizmos()
        {
            CreateAppropriateColliderAndSetValues();
        }

#endif
    }
}
