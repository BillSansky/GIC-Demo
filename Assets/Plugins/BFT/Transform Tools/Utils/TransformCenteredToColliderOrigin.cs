using UnityEngine;

namespace BFT
{
    public class TransformCenteredToColliderOrigin : MonoBehaviour
    {
        public Collider relativeCollider;

        private void Awake()
        {
            transform.position = relativeCollider.bounds.center;
        }
    }
}
