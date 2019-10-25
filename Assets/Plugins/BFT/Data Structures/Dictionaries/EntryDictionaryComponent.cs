using UnityEngine;

namespace BFT
{
    /// <summary>
    ///     Stupid copy of dictionary asset as a behavior
    /// </summary>
    /// <typeparam name="T">the type of data stored in the dictionary</typeparam>
    /// <typeparam name="T2"> the type of dictionary</typeparam>
    /// <typeparam name="T3">the type of value holder</typeparam>
    public abstract class EntryDictionaryComponent<T, T2> : MonoBehaviour, IValue<T2> where T2 : IEntryDictionary<T>
    {
        public T2 Dictionary;

        public T2 Value => Dictionary;
    }
}
