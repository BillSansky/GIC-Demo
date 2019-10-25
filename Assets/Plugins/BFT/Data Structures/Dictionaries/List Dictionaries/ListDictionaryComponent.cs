using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class ListDictionaryComponent<TKey, T> : MonoBehaviour, IListDictionary<TKey, T>
    {
        public Dictionary<TKey, List<T>> Dictionary = new Dictionary<TKey, List<T>>();

        public bool InitializaDictionaryOnEnable = true;

        public bool IsSynchronized => ((ICollection) Dictionary).IsSynchronized;

        public bool IsFixedSize => ((IDictionary) Dictionary).IsFixedSize;

        public IEqualityComparer<TKey> Comparer => Dictionary.Comparer;

        public int Count => Dictionary.Count;

        public Dictionary<TKey, List<T>>.KeyCollection Keys => Dictionary.Keys;

        public Dictionary<TKey, List<T>>.ValueCollection Values => Dictionary.Values;

        public bool IsReadOnly => false;

        public void Add(TKey key, T value)
        {
            if (Dictionary == null || key == null || value == null)
                return;

            if (!Dictionary.ContainsKey(key))
            {
                Dictionary.Add(key, new List<T>());
            }
            else if (Dictionary[key].Contains(value))
            {
                return;
            }

            Dictionary[key].Add(value);
        }

        public void Remove(TKey key, T value)
        {
            UnityEngine.Debug.AssertFormat(Dictionary.ContainsKey(key), this,
                "The key {0} does not exist in the dictionary when trying to remove value {1}", key, value);

            Dictionary?[key].Remove(value);
        }

        public bool ContainsKey(TKey key)
        {
            return Dictionary.ContainsKey(key);
        }

        public List<T> this[TKey key]
        {
            get => Dictionary[key];
            set => Dictionary[key] = value;
        }

        public IEnumerator<KeyValuePair<TKey, List<T>>> GetEnumerator()
        {
            return Dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            ((ICollection) Dictionary)?.CopyTo(array, index);
        }

        [OnInspectorGUI]
        public void RemoveNulls()
        {
            if (Dictionary == null)
                return;

            foreach (var pair in Dictionary)
            {
                pair.Value?.RemoveAll(_ => _ == null);
            }
        }

        public void OnEnable()
        {
            if (InitializaDictionaryOnEnable)
                Dictionary = new Dictionary<TKey, List<T>>();
        }

        public void Add(TKey key, List<T> value)
        {
            Dictionary?.Add(key, value);
        }

        public void Add(KeyValuePair<TKey, List<T>> item)
        {
            Dictionary?.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            Dictionary?.Clear();
        }

        public bool Contains(KeyValuePair<TKey, List<T>> item)
        {
            return Dictionary != null && Dictionary.ContainsKey(item.Key) && Dictionary[item.Key] == item.Value;
        }

        public void CopyTo(KeyValuePair<TKey, List<T>>[] array, int arrayIndex)
        {
            if (Dictionary == null)
                return;

            for (int i = arrayIndex; i < array.Length; i++)
            {
                Dictionary.AddOrReplace(array[i].Key, array[i].Value);
            }
        }

        public bool Remove(KeyValuePair<TKey, List<T>> item)
        {
            return Dictionary.Remove(item.Key);
        }

        public bool ContainsValue(List<T> value)
        {
            return Dictionary.ContainsValue(value);
        }


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Dictionary.GetObjectData(info, context);
        }

        public void OnDeserialization(object sender)
        {
            Dictionary.OnDeserialization(sender);
        }

        public bool Remove(TKey key)
        {
            return Dictionary.Remove(key);
        }

        public bool TryGetValue(TKey key, out List<T> value)
        {
            return Dictionary.TryGetValue(key, out value);
        }
    }
}
