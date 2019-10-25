using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    /// <summary>
    ///     A base class for a quick implementation of the interface:
    ///     represents a value for a key in a dictionary.
    ///     has a name id for a better user interface recognition
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    [Serializable]
    [HideLabel]
    [InlineProperty]
    public abstract class DictionaryEntry<T1> : IDictionaryEntry<T1>
    {
        [SerializeField, HideInInspector] private int dictionaryID;

        public int ID
        {
            get => dictionaryID;

            private set => dictionaryID = value;
        }

        public void RefreshDictionaryKey(int newID)
        {
            dictionaryID = newID;
        }

        public abstract string NameID { get; set; }
        public abstract T1 Data { get; set; }

        public abstract JsonData ExportJsonData();
        public abstract void ParseJsonData(JsonData data);
        public abstract void NotifyJSonDataDeleteRequest();
    }
}
