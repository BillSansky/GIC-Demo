using System;
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
    /// <typeparam name="T1">the type of dictionary value</typeparam>
    /// <typeparam name="T2">the type of dictionary entry</typeparam>
    /// <typeparam name="T3"> the type of dictionary</typeparam>
    [Serializable]
    public class ValueFromDictionary<T, T1, T2, T3> : IntValue, IValue<T>
        where T1 : EntryDictionaryValue<T, T2, T3>
        where T2 : class, IDictionaryEntry<T>
        where T3 : EntryDictionary<T, T2>
    {
        [FoldoutGroup("Value", false)] [SerializeField]
        private T defaultValue;

        [FormerlySerializedAs("referenceEntryDictionary")]
        [FormerlySerializedAs("referenceDictionary")]
        [SerializeField]
        private T1 dictionary;

        [HorizontalGroup]
        [HideIf("UseReference"), HideLabel, ShowInInspector, ValueDropdown("CurrentValueDropDown")]
        protected override int LocalValueInspected
        {
            get => LocalValue;
            set => LocalValue = value;
        }

        private ValueDropdownList<int> CurrentValueDropDown
        {
            get
            {
                if (ReferenceEntryDictionary != null)
                    return ReferenceEntryDictionary.ContentIDs;

                return new ValueDropdownList<int>();
            }
        }

        public T DictionaryValue

        {
            get
            {
                if (ReferenceEntryDictionary != null)
                    return ReferenceEntryDictionary.ContainsKey(ID) ? ReferenceEntryDictionary.Get(ID) : DefaultValue;
                return default(T);
            }

            //  set => ReferenceDictionary.Set(ID, value);
        }

        public int ID
        {
            get => Value;

            set => LocalValueInspected = value;
        }

        public IEntryDictionary<T> ReferenceEntryDictionary => dictionary.Value;

        public T DefaultValue => defaultValue;

        [FoldoutGroup("Value", false), ShowInInspector]
        T IValue<T>.Value => DictionaryValue;

        public override bool DrawRef(bool value, GUIContent label)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Dictionary Key");
            bool ret = base.DrawRef(value, label);
            EditorGUILayout.EndHorizontal();
            return ret;
        }
    }
}
