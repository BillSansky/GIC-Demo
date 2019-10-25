#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace BFT
{
    public static class HandleExt
    {
        private static readonly GUIStyle TempStyle = new GUIStyle();


        public static void Text(Vector3 position, string text, bool colorFromGizmos = false)
        {
            TempStyle.normal.textColor = colorFromGizmos ? Gizmos.color : Handles.color;
            Handles.Label(position, text, TempStyle);
        }
    }
}
#endif
