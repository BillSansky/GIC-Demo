using System.Linq;
using UnityEngine;

namespace BFT
{
    public static class CameraExt
    {
        public static bool IsVisible(this Camera cam, UnityEngine.Transform trans)
        {
            return cam.IsVisible(trans.position);
        }

        public static bool IsVisible(this Camera cam, Vector3 vec)
        {
            Vector3 pos = cam.WorldToViewportPoint(vec);

            return (pos.z >= 0 && pos.y <= 1 && pos.y >= 0 && pos.x <= 1 && pos.x >= 0);
        }

        public static bool IsFullyVisible(this Camera cam, SphereCollider col)
        {
            return GeometryUtility.CalculateFrustumPlanes(cam)
                .All(plane => !(plane.GetDistanceToPoint(col.transform.position) < col.radius));
        }

        public static bool IsFullyVisible(this Camera cam, BoxCollider col)
        {
            return col.Points().All(cam.IsVisible);
        }

        public static float DistanceToScreenCenter(this Camera cam, Vector3 vec)
        {
            Vector2 proj = cam.WorldToScreenPoint(vec);
            return (proj - (Screen.currentResolution.Extends() / 2)).magnitude;
        }
    }
}
