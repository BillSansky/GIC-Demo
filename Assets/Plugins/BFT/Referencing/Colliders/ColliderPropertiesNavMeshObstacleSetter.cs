using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace BFT
{
    public class ColliderPropertiesNavMeshObstacleSetter : SerializedMonoBehaviour, IValue<ColliderProperties>
    {
        [OnValueChanged("CreateAppropriateColliderAndSetValues"), HideIf("useColliderReference")]
        public IValue<ColliderProperties> ColliderProperties;

        [ShowIf("useColliderReference")] public Collider ColliderReference;

        private NavMeshObstacle obstacle;

        [SerializeField] private bool useColliderReference = false;


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

        private void CreateAppropriateColliderAndSetValues()
        {
            if ((ColliderProperties == null && !useColliderReference) ||
                (ColliderReference == null && useColliderReference))
                return;

            if (!obstacle)
                obstacle = GetComponent<NavMeshObstacle>();

            if (!obstacle)
                return;

            if (!useColliderReference)
            {
                switch (ColliderProperties.Value.ColliderType)
                {
                    case ColliderPropertyType.BOX:

                        obstacle.shape = NavMeshObstacleShape.Box;
                        obstacle.size = ColliderProperties.Value.HalfSize * 2;

                        break;
                    case ColliderPropertyType.SPHERE:
                        obstacle.shape = NavMeshObstacleShape.Capsule;
                        obstacle.radius = ColliderProperties.Value.Radius;
                        obstacle.height = 0;

                        break;
                    case ColliderPropertyType.CAPSULE:
                        obstacle.shape = NavMeshObstacleShape.Capsule;
                        obstacle.radius = ColliderProperties.Value.Radius;
                        obstacle.height = ColliderProperties.Value.Height;

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                obstacle.center = ColliderProperties.Value.Center;
            }
            else
            {
                if (ColliderReference is CapsuleCollider)
                {
                    CapsuleCollider capsuleCollider = ColliderReference as CapsuleCollider;
                    obstacle.shape = NavMeshObstacleShape.Capsule;
                    obstacle.radius = capsuleCollider.radius;
                    obstacle.height = capsuleCollider.height;
                    obstacle.center = capsuleCollider.center;
                }
                else if (ColliderReference is SphereCollider)
                {
                    SphereCollider sphereCollider = ColliderReference as SphereCollider;
                    obstacle.shape = NavMeshObstacleShape.Capsule;
                    obstacle.radius = sphereCollider.radius;
                    obstacle.center = sphereCollider.center;
                }
                else if (ColliderReference is BoxCollider)
                {
                    BoxCollider boxCollider = ColliderReference as BoxCollider;
                    obstacle.shape = NavMeshObstacleShape.Box;
                    obstacle.size = boxCollider.size;
                    obstacle.center = boxCollider.center;
                }
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
