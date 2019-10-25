using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class ColliderPropertiesTransformSetter : SerializedMonoBehaviour
    {
        public IValue<ColliderProperties> ColliderProperties;
        public IValue<float> scaleModificator;

        public bool SetLocalPosition = true;

        void Awake()
        {
            SetValues();
        }

        [Button(ButtonSizes.Medium)]
        private void SetValues()
        {
            SetScale();
            if (SetLocalPosition)
                SetPosition();
        }

        [OnInspectorGUI]
        private void InspectorUpdate()
        {
            if (!Application.isPlaying)
            {
                SetValues();
            }
        }

        private void SetScale()
        {
            if (ColliderProperties == null)
                return;

            switch (ColliderProperties.Value.ColliderType)
            {
                case ColliderPropertyType.BOX:
                    transform.localScale = ColliderProperties.Value.HalfSize * 2 *
                                           (scaleModificator != null ? scaleModificator.Value : 1);
                    break;
                case ColliderPropertyType.SPHERE:
                case ColliderPropertyType.CAPSULE:
                    transform.localScale = Vector3.one * ColliderProperties.Value.Radius * 2 *
                                           (scaleModificator != null ? scaleModificator.Value : 1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetPosition()
        {
            if (ColliderProperties == null)
                return;

            transform.localPosition = ColliderProperties.Value.Center;
        }

#if UNITY_EDITOR

        void OnDrawGizmos()
        {
            InspectorUpdate();
        }

#endif
    }
}
