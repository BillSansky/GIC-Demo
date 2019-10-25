using UnityEngine;

namespace BFT
{
    public static class ColliderExt
    {
        public static bool ContainsPosition(this Collider collider, Vector3 position)
        {
            return collider.ClosestPoint(position) == position;
        }

        public static bool ContainsBounds(this Collider collider, Bounds bounds)
        {
            return (collider.ContainsPosition(bounds.BottomLeftBack()) &&
                    collider.ContainsPosition(bounds.BottomLeftFront()) &&
                    collider.ContainsPosition(bounds.BottomRightBack()) &&
                    collider.ContainsPosition(bounds.BottomRightFront()) &&
                    collider.ContainsPosition(bounds.TopLeftBack()) &&
                    collider.ContainsPosition(bounds.TopLeftFront()) &&
                    collider.ContainsPosition(bounds.TopRightBack()) &&
                    collider.ContainsPosition(bounds.TopRightFront()));
        }
    }
}
