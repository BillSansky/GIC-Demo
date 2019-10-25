#if UNITY_EDITOR
#endif
using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace BFT
{
    /// <summary>
    ///     Similar to a GenericValue, but with the ability to set the value too
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    [InlineProperty]
    [HideReferenceObjectPicker]
    public class GenericVariable<T> : IVariable<T>
    {
        [HideIf("UseReference"), HideLabel] [HorizontalGroup]
        public T LocalValue;

        [FormerlySerializedAs("Variable")]
        [ShowIf("UseReference"), HideLabel, OnValueChanged("CheckType")]
        [HorizontalGroup]
        public UnityEngine.Object Reference;

        [HideLabel, CustomValueDrawer("DrawRef")] [HorizontalGroup(Width = 20)]
        public bool UseReference = false;

        public GenericVariable()
        {
        }

        public GenericVariable(T localValue)
        {
            LocalValue = localValue;
        }

        private ValueDropdownList<bool> ReferenceDropDown =>
            new ValueDropdownList<bool>()
            {
                {"Value", false},
                {"Reference", true}
            };

        public T Value
        {
            get
            {
                if (!UseReference)
                {
                    return LocalValue;
                }

                if (!Reference)
                    return default;

#if UNITY_EDITOR

                stackOver++;

                if (stackOver > 50)
                {
                    UnityEngine.Debug.LogError(
                        "The variable is linking to itself at some point: this is not allowed, the reference usage has been disabled",
                        Reference);
                    Reference = null;
                    UseReference = false;
                    stackOver = 0;
                    return default;
                }

                T value = ((IValue<T>) Reference).Value;

                stackOver = 0;

                return value;

#endif

                return ((IValue<T>) Reference).Value;
            }

            set
            {
                if (!UseReference)
                {
                    LocalValue = value;
                }
                else
                {
                    ((IVariable<T>) Reference).Value = value;
                }
            }
        }

        public void UseAndSetReference(UnityEngine.Object reference)
        {
            UseReference = true;
            Reference = reference;
        }
#if UNITY_EDITOR
        public bool DrawRef(bool value, GUIContent label)
        {
            if (popupStyle == null)
            {
                popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"))
                {
                    imagePosition = ImagePosition.ImageOnly, alignment = TextAnchor.MiddleCenter, fixedHeight = 20,
                    fixedWidth = 20,
                };
            }

            if (label != null)
                EditorGUILayout.LabelField(label);
            bool useConstant = !value;

            int result = EditorGUILayout.Popup(useConstant ? 0 : 1, PopupOptions, popupStyle, GUILayout.MinWidth(10),
                GUILayout.MinHeight(15));

            return result != 0;
        }

        /// <summary> Cached style to use to draw the popup button. </summary>
        private GUIStyle popupStyle;

        private static readonly string[] PopupOptions =
            {"Use Value", $"Use Reference (Type Variable<{typeof(T).Name}>)"};
#endif

#if UNITY_EDITOR

        protected virtual void CheckType()
        {
            InterfaceUtils.GetObjectAfterTypeCheck<IVariable<T>>(ref Reference);

            var type = Reference.GetType();

            var fields = type.GetAllFields();

            foreach (var field in fields)
            {
                var value = field.GetValue(Reference);
                if (value == this)
                {
                    UnityEngine.Debug.LogWarning("Self referencing is not allowed!", Reference);
                    Reference = null;
                    return;
                }
            }
        }

        private int stackOver = 0;

#endif
    }
}
