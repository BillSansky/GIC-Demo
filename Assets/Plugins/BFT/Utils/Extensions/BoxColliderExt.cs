using System.Collections.Generic;
using UnityEngine;

namespace BFT
{
    public static class BoxColliderExt
    {
        public static IEnumerable<Vector3> Points(this BoxCollider col)
        {
            yield return col.center + col.size / 2;
            yield return col.center + col.size.Mult(1, -1, 1) / 2;
            yield return col.center + col.size.Mult(-1, 1, 1) / 2;
            yield return col.center + col.size.Mult(1, 1, -1) / 2;
            yield return col.center + col.size.Mult(1, -1, -1) / 2;
            yield return col.center + col.size.Mult(-1, 1, -1) / 2;
            yield return col.center + col.size.Mult(-1, -1, 1) / 2;
            yield return col.center + col.size.Mult(-1, -1, -1) / 2;
        }


        public static Vector3[] WorldPoints(this BoxCollider col)
        {
            return new[]
            {
                col.transform.TransformPoint(col.center + col.size / 2),
                col.transform.TransformPoint(col.center + col.size.Mult(1, -1, 1) / 2),
                col.transform.TransformPoint(col.center + col.size.Mult(-1, 1, 1) / 2),
                col.transform.TransformPoint(col.center + col.size.Mult(1, 1, -1) / 2),
                col.transform.TransformPoint(col.center + col.size.Mult(1, -1, -1) / 2),
                col.transform.TransformPoint(col.center + col.size.Mult(-1, 1, -1) / 2),
                col.transform.TransformPoint(col.center + col.size.Mult(-1, -1, 1) / 2),
                col.transform.TransformPoint(col.center + col.size.Mult(-1, -1, -1) / 2)
            };
        }

        public static Vector3 TopRightBack(this BoxCollider bounds)
        {
            return bounds.transform.TransformPoint(bounds.center + bounds.size / 2);
        }

        public static Vector3 TopLeftFront(this BoxCollider bounds)
        {
            return bounds.transform.TransformPoint(bounds.center + (bounds.size / 2).Mult(-1, 1, -1));
        }

        public static Vector3 TopRightFront(this BoxCollider bounds)
        {
            return bounds.transform.TransformPoint(bounds.center + (bounds.size / 2).Mult(1, 1, -1));
        }

        public static Vector3 BottomRightFront(this BoxCollider bounds)
        {
            return bounds.transform.TransformPoint(bounds.center + (bounds.size / 2).Mult(1, -1, -1));
        }

        public static Vector3 BottomLeftFront(this BoxCollider bounds)
        {
            return bounds.transform.TransformPoint(bounds.center + (bounds.size / 2).Mult(-1, -1, -1));
        }

        public static Vector3 TopLeftBack(this BoxCollider bounds)
        {
            return bounds.transform.TransformPoint(bounds.center + (bounds.size / 2).Mult(-1, 1, 1));
        }

        public static Vector3 BottomLeftBack(this BoxCollider bounds)
        {
            return bounds.transform.TransformPoint(bounds.center + (bounds.size / 2).Mult(-1, -1, 1));
        }

        public static Vector3 BottomRightBack(this BoxCollider bounds)
        {
            return bounds.transform.TransformPoint(bounds.center + (bounds.size / 2).Mult(1, -1, 1));
        }

        public static Vector3 RandomPointInBox(this BoxCollider box)
        {
            Vector3 rightSideRand = RandomExt.RandomVectorInQuad(box.TopRightBack(), box.TopRightFront(),
                box.BottomRightBack(), box.BottomRightFront());
            Vector3 leftSideRand = RandomExt.RandomVectorInQuad(box.TopLeftBack(), box.TopLeftFront(),
                box.BottomLeftBack(), box.BottomLeftFront());
            return RandomExt.RandomVectorBetween(rightSideRand, leftSideRand);
        }
    }
}
