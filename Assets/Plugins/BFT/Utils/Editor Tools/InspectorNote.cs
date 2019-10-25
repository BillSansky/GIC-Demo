using Sirenix.OdinInspector;
using UnityEditor;
#if UNITY_EDITOR
using UnityEngine;

#if UNITY_EDITOR
#endif

namespace BFT
{
    public class InspectorNote : MonoBehaviour
    {
        [TextArea(5, 50)] public string Note;

        [ShowIf("ShowOnScene")] public bool ShowAllTheTime = false;

        public bool ShowOnScene = true;

        private GUIStyle style;
        private Texture2D tex;

#if UNITY_EDITOR

        public void OnDrawGizmosSelected()
        {
            if (ShowOnScene && Selection.activeGameObject == gameObject)
            {
                DrawNote();
            }
        }

        public void OnDrawGizmos()
        {
            if (ShowOnScene && ShowAllTheTime)
            {
                DrawNote();
            }
        }

        private void DrawNote()
        {
            if (style == null || tex == null || style.normal.background != tex)
            {
                tex = TextureUtils.ColorTexture(1, 1, ColorExt.HTMLColor("#373737"));
                tex.hideFlags = HideFlags.HideAndDontSave;
                DontDestroyOnLoad(tex);
                style = new GUIStyle()
                {
                    stretchWidth = true,
                    stretchHeight = true,
                    padding = new RectOffset(3, 3, 3, 3),
                    normal = {background = tex, textColor = Color.white}
                };
            }

            Handles.color = Color.white;
            Handles.Label(transform.position,
                new GUIContent(Note, TextureUtils.ColorTexture(10, 10, ColorExt.HTMLColor("#373737"))), style);
        }
#endif
    }
}


#endif
