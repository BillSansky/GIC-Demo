using UnityEngine;

namespace BFT
{
    public static class Vector2Ext
    {
        public static Vector2 Ortho(this Vector2 use)
        {
            return new Vector2(-use.y, use.x);
        }

        public static bool IsBoxed(this Vector2 use, Vector2 topleft, Vector2 bottomright)
        {
            return (use.x >= topleft.x && use.x <= bottomright.x && use.y >= bottomright.y && use.y <= topleft.y);
        }

        public static bool IsScreenBoxed(this Vector2 use)
        {
            return
                use.x >= 0 && use.x <= Screen.currentResolution.width && use.y >= 0 && use.y <=
                Screen.currentResolution.height;
        }

        /// <summary>
        ///     the number indicated the position of the new coordinate
        /// </summary>
        /// <param name="use"></param>
        /// <returns></returns>
        public static Vector3 V3Equivalent(this Vector2 use, int indexX, int indexY)
        {
            Vector3 ret = new Vector3();
            switch (indexX)
            {
                case 0:
                    ret.x = use.x;
                    break;
                case 1:
                    ret.y = use.x;
                    break;
                case 2:
                    ret.z = use.x;
                    break;
            }

            switch (indexY)
            {
                case 0:
                    ret.x = use.y;
                    break;
                case 1:
                    ret.y = use.y;
                    break;
                case 2:
                    ret.z = use.y;
                    break;
            }

            return ret;
        }

        /// <summary>
        ///     the number indicated the position of the new coordinate
        /// </summary>
        /// <param name="use"></param>
        /// <returns></returns>
        public static Vector3 V3Equivalent(this Vector2 use, float extraValue, int indexX, int indexY)
        {
            Vector3 ret = new Vector3();
            switch (indexX)
            {
                case 0:
                    ret.x = use.x;
                    switch (indexY)
                    {
                        case 1:
                            ret.y = use.y;
                            ret.z = extraValue;
                            break;
                        case 2:
                            ret.z = use.y;
                            ret.y = extraValue;
                            break;
                    }

                    break;
                case 1:
                    ret.y = use.x;
                    switch (indexY)
                    {
                        case 0:
                            ret.x = use.y;
                            ret.z = extraValue;
                            break;
                        case 2:
                            ret.x = extraValue;
                            ret.z = use.y;
                            break;
                    }

                    break;
                case 2:
                    ret.z = use.x;
                    switch (indexY)
                    {
                        case 0:
                            ret.x = use.y;
                            ret.y = extraValue;
                            break;
                        case 1:
                            ret.y = use.y;
                            ret.x = extraValue;
                            break;
                    }

                    break;
            }


            return ret;
        }


        public static Vector2 NegateY(this Vector2 use)
        {
            return new Vector2(use.x, -use.y);
        }

        public static float ParametricRelation(this Vector2 use, Vector2 from, Vector2 to)
        {
            return (Vector3.Project(use, (Vector3) (to - from)) - (Vector3) from).magnitude / (to - from).magnitude;
        }

        public static Vector2 Mult(this Vector2 one, Vector2 other)
        {
            return new Vector2(one.x * other.x, one.y * other.y);
        }

        public static Vector2 Mult(this Vector2 one, float x, float y)
        {
            return new Vector2(one.x * x, one.y * y);
        }

        public static Vector2 Div(this Vector2 one, Vector2 other)
        {
            return new Vector2(one.x / other.x, one.y / other.y);
        }
    }
}
