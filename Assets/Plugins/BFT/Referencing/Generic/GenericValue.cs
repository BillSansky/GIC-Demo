using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

#if UNITY_EDITOR
#endif

namespace BFT
{
    /// <summary>
    ///     Can hold a reference to an object that implements the IValue iterface of the type specified, or a direct value of
    ///     the type specified
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    [InlineProperty]
    public class GenericValue<T> : IValue<T>
    {
        [SerializeField, HideInInspector] public T LocalValue;

        [HideLabel, CustomValueDrawer("DrawRef")] [HorizontalGroup(Width = 20)]
        public bool UseReference = false;

        [FormerlySerializedAs("Variable")]
        [HorizontalGroup]
        [ShowIf("UseReference"), HideLabel, OnValueChanged("CheckType")]
        public UnityEngine.Object Reference;


        public GenericValue()
        {
        }

        public GenericValue(T localValue)
        {
            this.LocalValue = localValue;
        }

        [HorizontalGroup]
        [HideIf("UseReference"), HideLabel, ShowInInspector]
        protected virtual T LocalValueInspected
        {
            get => LocalValue;
            set => LocalValue = value;
        }

        private bool IsVariableDefined => UseReference && !Reference;

        [ShowInInspector]
        [ShowIf("UseReference")]
        [InfoBox("$RefProblemMessage", InfoMessageType.Warning, GUIAlwaysEnabled = true,
            VisibleIf = "IsVariableDefined")]
        public virtual T Value
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
        }

#if UNITY_EDITOR
        protected virtual void CheckType()
        {
            InterfaceUtils.GetObjectAfterTypeCheck<IValue<T>>(ref Reference);

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

            if (!Reference)
                RefProblemMessage =
                    $"The Variable linked was of the wrong type, expecting a {typeof(T).Name} Value";
        }

#endif
        public void SetAndUseReference(UnityEngine.Object reference)
        {
            Reference = reference;
            UseReference = true;
        }

#if UNITY_EDITOR
        private ValueDropdownList<bool> ReferenceDropDown =>
            new ValueDropdownList<bool>()
            {
                {"Value", false},
                {"Reference", true}
            };

        public virtual bool DrawRef(bool value, GUIContent label)
        {
            if (popupStyle == null)
            {
                popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"))
                {
                    imagePosition = ImagePosition.ImageOnly, alignment = TextAnchor.MiddleCenter, fixedHeight = 20,
                    fixedWidth = 20,
                };
            }

            /*  if (label != null)
              EditorGUILayout.LabelField(label);*/
            bool useConstant = !value;

            int result = EditorGUILayout.Popup(useConstant ? 0 : 1, PopupOptions, popupStyle, GUILayout.MinWidth(10),
                GUILayout.MinHeight(15));

            return result != 0;
        }

        /// <summary> Cached style to use to draw the popup button. </summary>
        private static GUIStyle popupStyle;

        private static readonly string[] PopupOptions =
            {"Use Value", $"Use Reference (Type Value<{typeof(T).Name}>)"};

#endif


#if UNITY_EDITOR

        private string RefProblemMessage = $"Please Set a Reference of type {typeof(T).Name}";
        private int stackOver = 0;
#endif
    }
}