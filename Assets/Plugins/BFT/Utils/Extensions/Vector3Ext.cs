using System.Collections.Generic;
using UnityEngine;

namespace BFT
{
    public static class Vector3Ext
    {
        public delegate Vector3 ObjectPosition<Obj>(Obj obj);

        public static Vector2 GetPlanar(this Vector3 use, int x, int y)
        {
            float xf = 0;
            float yf = 0;

            switch (x)
            {
                case 0:
                    xf = use.x;
                    break;
                case 1:
                    xf = use.y;
                    break;
                case 2:
                    xf = use.z;
                    break;
            }

            switch (y)
            {
                case 0:
                    yf = use.y;
                    break;
                case 1:
                    yf = use.y;
                    break;
                case 2:
                    yf = use.z;
                    break;
            }

            return new Vector2(xf, yf);
        }

        

        /// <summary>
        ///     first index then value
        /// </summary>
        /// <param name="use"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Vector3 GetSEet(this Vector3 use, params float[] coords)
        {
            Vector3 outV = use;

            switch ((int) coords[0])
            {
                case 0:
                    outV.x = coords[1];
                    break;
                case 1:
                    outV.y = coords[1];
                    break;
                case 2:
                    outV.z = coords[1];
                    break;
            }

            if (coords.Length > 2)
            {
                switch ((int) coords[2])
                {
                    case 0:
                        outV.x = coords[3];
                        break;
                    case 1:
                        outV.y = coords[3];
                        break;
                    case 2:
                        outV.z = coords[3];
                        break;
                }
            }

            if (coords.Length > 4)
            {
                switch ((int) coords[4])
                {
                    case 0:
                        outV.x = coords[5];
                        break;
                    case 1:
                        outV.y = coords[5];
                        break;
                    case 2:
                        outV.z = coords[5];
                        break;
                }
            }

            return outV;
        }

        public static bool IsOpposed(this Vector3 vec, Vector3 other, Vector3 up)
        {
            return Mathf.Abs(MathExt.SignedVectorAngle(vec, other, up)) > 90;
        }

        public static bool IsBoxed(this Vector3 use, Vector3 topfrontleft, Vector3 bottomrearright)
        {
            return (use.x >= topfrontleft.x && use.x <= bottomrearright.x && use.y >= bottomrearright.y &&
                    use.y <= topfrontleft.y && use.z >= topfrontleft.z && use.z <= bottomrearright.z);
        }

        public static Vector3 GetClampedMagnitude(this Vector3 vec, float minMagnitude, float maxMagnitude)
        {
            if (vec.magnitude < minMagnitude)
                return vec.normalized * minMagnitude;

            return Vector3.ClampMagnitude(vec, maxMagnitude);
        }

        public static Vector3 GetClampedAngleTo(this Vector3 vec, Vector3 from, Vector3 up, float maxAngle)
        {
            float angle = MathExt.SignedVectorAngle(from, vec, up);
            if (Mathf.Abs(angle) > maxAngle)
            {
                Quaternion rot;
                //then the rotation needs to operate counterclockwise
                if (angle < 0)
                {
                    rot = Quaternion.AngleAxis(-maxAngle, up);
                }
                else
                {
                    rot = Quaternion.AngleAxis(maxAngle, up);
                }

                vec = rot * from.normalized * vec.magnitude;
                //TODO negate if reversed (add an option)
            }

            return vec;
        }

        public static Vector3 ClosestPoint(this Vector3 use, IEnumerable<Vector3> vectors)
        {
            float minDistance = float.MaxValue;
            Vector3 currentClosest = new Vector3();

            foreach (Vector3 vec in vectors)
            {
                if (Vector3.Distance(use, vec) < minDistance)
                {
                    minDistance = Vector3.Distance(use, vec);
                    currentClosest = vec;
                }
            }

            return currentClosest;
        }

        public static Obj ClosestObject<Obj>(this Vector3 use, IEnumerable<Obj> vectors, ObjectPosition<Obj> positionner)
        {
            float minDistance = float.MaxValue;
            Obj currentClosest = default(Obj);
            Vector3 vec;
            foreach (Obj obj in vectors)
            {
                vec = positionner(obj);
                if (Vector3.Distance(use, vec) < minDistance)
                {
                    minDistance = Vector3.Distance(use, vec);
                    currentClosest = obj;
                }
            }

            return currentClosest;
        }

        public static float ParametricRelation(this Vector3 use, Vector3 from, Vector3 to)
        {
            return (Vector3.Project(use, to - from) - from).magnitude / (to - from).magnitude;
        }

        public static Vector3 ProjectOnSegment(this Vector3 use, Vector3 from, Vector3 to)
        {
            return Vector3.Project(use - from, to - from) + from;
        }


        public static Vector3 Ortho(this Vector3 use, Vector3 axis)
        {
            return Quaternion.AngleAxis(90, axis) * use;
        }

        public static Vector3 Rounded(this Vector3 use)
        {
            return new Vector3(Mathf.Round(use.x), Mathf.Round(use.y), Mathf.Round(use.z));
        }

        public static Vector3 Rounded(this Vector3 use, int decimals)
        {
            return new Vector3((float) System.Math.Round(use.x, decimals, System.MidpointRounding.AwayFromZero)
                , (float) System.Math.Round(use.y, decimals, System.MidpointRounding.AwayFromZero)
                , (float) System.Math.Round(use.z, decimals, System.MidpointRounding.AwayFromZero));
        }

        public static Vector3 Floored(this Vector3 use)
        {
            return new Vector3((int) use.x, (int) use.y, (int) use.z);
        }

        public static Vector3 Mult(this Vector3 one, Vector3 two)
        {
            return new Vector3(one.x * two.x, one.y * two.y, one.z * two.z);
        }

        public static Vector3 Mult(this Vector3 one, float x, float y, float z)
        {
            return new Vector3(one.x * x, one.y * y, one.z * z);
        }

        public static Vector3 Div(this Vector3 one, Vector3 two)
        {
            return new Vector3(one.x / two.x, one.y / two.y, one.z / two.z);
        }

        public static Vector4 Vector4(this Vector3 one, float w)
        {
            return new Vector4(one.x, one.y, one.z, w);
        }

        public static bool IsOneOfCoordBigger(this Vector3 one, Vector3 two)
        {
            return one.x > two.x || one.y > two.y || one.z > two.z;
        }

        public static bool IsMagnitudeUnder(this Vector3 from, float threshold)
        {
            return (from.sqrMagnitude < threshold * threshold);
        }

        public static bool IsMagnitudeUnderEqual(this Vector3 from, float threshold)
        {
            return (from.sqrMagnitude <= threshold * threshold);
        }

        public static bool IsMagnitudeOver(this Vector3 from, float threshold)
        {
            return (from.sqrMagnitude > threshold * threshold);
        }

        public static bool IsMagnitudeOverEqual(this Vector3 from, float threshold)
        {
            return (from.sqrMagnitude >= threshold * threshold);
        }

        public static bool IsDistanceUnder(this Vector3 vec, Vector3 vec2, float distanceThreshold)
        {
            return (vec - vec2).IsMagnitudeUnder(distanceThreshold);
        }

        public static float Distance(this Vector3 vec, Vector3 vec2)
        {
            return (vec - vec2).magnitude;
        }

        public static Vector3 DistanceToVector(this Vector3 vec, Vector3 to)
        {
            return (to - vec);
        }

        public static Vector3 DistanceFromVector(this Vector3 vec, Vector3 from)
        {
            return vec - from;
        }

        public static Vector3 WithSetY(this Vector3 from, float newY)
        {
            from.y = newY;
            return from;
        }

        public static Vector3 WithSetX(this Vector3 from, float newX)
        {
            from.x = newX;
            return from;
        }

        public static Vector3 WithSetZ(this Vector3 from, float newZ)
        {
            from.z = newZ;
            return from;
        }

        public static Vector3 RotatePointAroundPivot(this Vector3 point, Vector3 pivot, Vector3 angles)
        {
            Vector3 dir = point - pivot; // get point direction relative to pivot
            dir = Quaternion.Euler(angles) * dir; // rotate it
            point = dir + pivot; // calculate rotated point
            return point; // return it
        }

        public static Vector3[] ApplyTranslation(this Vector3[] directions, Vector3 translation)
        {
            for (var i = 0; i < directions.Length; i++)
                directions[i] += translation;
            return directions;
        }

        public static Vector3[] ApplyRotation(this Vector3[] directions, Vector3 rotation)
        {
            var vectorZero = Vector3.zero;
            for (var i = 0; i < directions.Length; i++)
                directions[i] = directions[i].RotatePointAroundPivot(vectorZero, rotation);

            return directions;
        }

        public static Vector3[] ApplyRotationAroundPoint(this Vector3[] directions, Vector3 rotation, Vector3 point)
        {
            for (var i = 0; i < directions.Length; i++)
                directions[i] = directions[i].RotatePointAroundPivot(point, rotation);

            return directions;
        }

        /// <summary>
        ///     returns the highest value between x, y and z
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static float MaxComponent(this Vector3 vec)
        {
            return Mathf.Max(Mathf.Max(vec.x, vec.y), vec.z);
        }
    }
}
