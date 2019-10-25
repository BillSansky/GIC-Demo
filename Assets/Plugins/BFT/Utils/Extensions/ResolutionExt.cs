using UnityEngine;

namespace BFT
{
    public static class ResolutionExt
    {
        public static Vector2 Extends(this Resolution res)
        {
            return new Vector2(res.width, res.height);
        }

        public static Vector2 Center(this Resolution res)
        {
            return Extends(res) / 2;
        }
    }
}
