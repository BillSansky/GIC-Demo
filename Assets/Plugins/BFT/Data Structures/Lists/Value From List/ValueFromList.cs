using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace BFT
{
    /// <summary>
    ///     Gives a value from a dictionary given a key id
    /// </summary>
    /// <typeparam name="T"> the type of data</typeparam>
    /// <typeparam name="T1">the type of dictionary</typeparam>
    [Serializable]
    public class ValueFromList<T, T1> : IntValue, IValue<T> where T1 : IValue<List<T>>
    {
        [FoldoutGroup("Value", false)] [SerializeField]
        private T defaultValue;


        [FormerlySerializedAs("referenceDictionary")] [SerializeField]
        private T1 List;

        [HorizontalGroup]
        [HideIf("UseReference"), HideLabel, ShowInInspector, ValueDropdown("CurrentValueDropDown")]
        protected override int LocalValueInspected
        {
            get => LocalValue;
            set => LocalValue = value;
        }

        public int ID
        {
            get => Value;

            set => LocalValueInspected = value;
        }

        public T DefaultValue => defaultValue;

        public T ListValue

        {
            get
            {
                if (List != null)
                    return (ID >= 0 && ID < List.Value.Count) ? List.Value[ID] : DefaultValue;
                return default(T);
            }

            //  set => ReferenceDictionary.Set(ID, value);
        }

        [FoldoutGroup("Value", false), ShowInInspector]
        T IValue<T>.Value => ListValue;

        public override bool DrawRef(bool value, GUIContent label)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("List Key");
            bool ret = base.DrawRef(value, label);
            EditorGUILayout.EndHorizontal();
            return ret;
        }
    }
}
