using System;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
#endif

namespace BFT
{
    public class LayerUtils
    {
        public class LayerAttribute : PropertyAttribute
        {
        }

        [Serializable]
        public class LayerWrapper
        {
            [SerializeField, Layer] public int layer = 0;
        }

#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(LayerAttribute))]
        class LayerAttributeEditor : PropertyDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                property.intValue = EditorGUI.LayerField(position, label, property.intValue);
            }
        }
#endif
    }
}
