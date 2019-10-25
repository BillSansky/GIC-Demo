using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    /// <summary>
    ///     uses a value from a group of values using a drop down linked to an int ID
    /// </summary>
    /// <typeparam name="T">the type of value returned</typeparam>
    /// <typeparam name="T1">the value from dictionary type</typeparam>
    /// <typeparam name="T2">the type of dictionary value</typeparam>
    /// <typeparam name="T3">The type of entry data</typeparam>
    /// <typeparam name="T4">The type of dictionary</typeparam>
    public class ValueFromDictionaryComponent<T, T1, T2,T3,T4> : MonoBehaviour,IValue<T>
        where T1 : ValueFromDictionary<T, T2,T3,T4> where T2 : EntryDictionaryValue<T, T3, T4> where T3 : class, IDictionaryEntry<T> where T4 : EntryDictionary<T, T3>
    {
        [HideLabel] public T1 entryValue;

        public T Value => entryValue.DictionaryValue;
        public int ID => entryValue.ID;
        public IEntryDictionary<T> ReferenceEntryDictionary => entryValue.ReferenceEntryDictionary;
        public T DefaultValue => entryValue.DefaultValue;
    }
}
