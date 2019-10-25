using System.Collections.Generic;
using UnityEngine;

namespace BFT
{
    public static class BoundsExt
    {
        public static Bounds EncapsulateColliders(this Bounds bounds, IEnumerable<Collider> colliders)
        {
            foreach (Collider col in colliders)
            {
                bounds.Encapsulate(col.bounds.FurthestPoint(bounds.center));
            }

            return bounds;
        }

        public static Vector3 FurthestPoint(this Bounds bounds, Vector3 toCompare)
        {
            Vector3 distance = (bounds.ClosestPoint(toCompare) - bounds.center);

            if (distance.x > 0)
            {
                //then left side
                if (distance.y > 0)
                {
                    // then bottom
                    if (distance.z > 0)
                    {
                        //then front
                        return bounds.BottomLeftFront();
                    }
                    else
                    {
                        //then back
                        return bounds.BottomLeftBack();
                    }
                }
                else
                {
                    //then top
                    if (distance.z > 0)
                    {
                        //then front
                        return bounds.TopLeftFront();
                    }
                    else
                    {
                        //then back
                        return bounds.TopLeftBack();
                    }
                }
            }
            else
            {
                //then right side
                if (distance.y > 0)
                {
                    // then bottom
                    if (distance.z > 0)
                    {
                        //then front
                        return bounds.BottomRightFront();
                    }
                    else
                    {
                        //then back
                        return bounds.BottomRightBack();
                    }
                }
                else
                {
                    //then top
                    if (distance.z > 0)
                    {
                        //then front
                        return bounds.TopRightFront();
                    }
                    else
                    {
                        //then back
                        return bounds.TopRightBack();
                    }
                }
            }
        }

        public static Vector3 TopRightBack(this Bounds bounds)
        {
            return bounds.center + bounds.extents;
        }

        public static Vector3 TopLeftFront(this Bounds bounds)
        {
            return bounds.center + bounds.extents.Mult(-1, 1, -1);
        }

        public static Vector3 TopRightFront(this Bounds bounds)
        {
            return bounds.center + bounds.extents.Mult(1, 1, -1);
        }

        public static Vector3 BottomRightFront(this Bounds bounds)
        {
            return bounds.center + bounds.extents.Mult(1, -1, -1);
        }

        public static Vector3 BottomLeftFront(this Bounds bounds)
        {
            return bounds.center + bounds.extents.Mult(-1, -1, -1);
        }

        public static Vector3 TopLeftBack(this Bounds bounds)
        {
            return bounds.center + bounds.extents.Mult(-1, 1, 1);
        }

        public static Vector3 BottomLeftBack(this Bounds bounds)
        {
            return bounds.center + bounds.extents.Mult(-1, -1, 1);
        }

        public static Vector3 BottomRightBack(this Bounds bounds)
        {
            return bounds.center + bounds.extents.Mult(1, -1, 1);
        }

        public static IEnumerable<Vector3> Points(this Bounds bound)
        {
            yield return bound.BottomLeftBack();
            yield return bound.BottomLeftFront();
            yield return bound.BottomRightBack();
            yield return bound.BottomRightFront();
            yield return bound.TopLeftBack();
            yield return bound.TopLeftFront();
            yield return bound.TopRightBack();
            yield return bound.TopRightFront();
        }

        public static Vector3 RandomPointInBounds(this Bounds bound)
        {
            Vector3 rightSideRand = RandomExt.RandomVectorInQuad(bound.TopRightBack(), bound.TopRightFront(),
                bound.BottomRightBack(), bound.BottomRightFront());
            Vector3 leftSideRand = RandomExt.RandomVectorInQuad(bound.TopLeftBack(), bound.TopLeftFront(),
                bound.BottomLeftBack(), bound.BottomLeftFront());
            return RandomExt.RandomVectorBetween(rightSideRand, leftSideRand);
        }
    }
}
