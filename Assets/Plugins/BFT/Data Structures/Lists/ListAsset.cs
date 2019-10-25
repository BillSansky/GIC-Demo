using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class ListAsset<T> : SerializedScriptableObject, IList<T>, IValue<List<T>>
    {
        [SerializeField] private List<T> list;

        public bool IsSynchronized => ((ICollection) list).IsSynchronized;

        public object SyncRoot => ((ICollection) list).SyncRoot;

        public bool IsFixedSize => ((IList) list).IsFixedSize;

        public int Capacity
        {
            get => list.Capacity;

            set => list.Capacity = value;
        }

        public void Add(T item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int IndexOf(T item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            list.Insert(index, item);
        }

        public bool Remove(T item)
        {
            return list.Remove(item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public int Count => list.Count;

        public T this[int index]
        {
            get => list[index];

            set => list[index] = value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public bool IsReadOnly => false;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public List<T> Value => list;

        public void CopyTo(Array array, int index)
        {
            ((ICollection) list).CopyTo(array, index);
        }

        public int Add(object value)
        {
            return ((IList) list).Add(value);
        }

        public bool Contains(object value)
        {
            return ((IList) list).Contains(value);
        }

        public int IndexOf(object value)
        {
            return ((IList) list).IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            ((IList) list).Insert(index, value);
        }

        public void Remove(object value)
        {
            ((IList) list).Remove(value);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            list.AddRange(collection);
        }

        public ReadOnlyCollection<T> AsReadOnly()
        {
            return list.AsReadOnly();
        }

        public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
        {
            return list.BinarySearch(index, count, item, comparer);
        }

        public int BinarySearch(T item)
        {
            return list.BinarySearch(item);
        }

        public int BinarySearch(T item, IComparer<T> comparer)
        {
            return list.BinarySearch(item, comparer);
        }

        public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
        {
            return list.ConvertAll(converter);
        }

        public void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            list.CopyTo(index, array, arrayIndex, count);
        }

        public void CopyTo(T[] array)
        {
            list.CopyTo(array);
        }

        public bool Exists(Predicate<T> match)
        {
            return list.Exists(match);
        }

        public T Find(Predicate<T> match)
        {
            return list.Find(match);
        }

        public List<T> FindAll(Predicate<T> match)
        {
            return list.FindAll(match);
        }

        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            return list.FindIndex(startIndex, count, match);
        }

        public int FindIndex(int startIndex, Predicate<T> match)
        {
            return list.FindIndex(startIndex, match);
        }

        public int FindIndex(Predicate<T> match)
        {
            return list.FindIndex(match);
        }

        public T FindLast(Predicate<T> match)
        {
            return list.FindLast(match);
        }

        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            return list.FindLastIndex(startIndex, count, match);
        }

        public int FindLastIndex(int startIndex, Predicate<T> match)
        {
            return list.FindLastIndex(startIndex, match);
        }

        public int FindLastIndex(Predicate<T> match)
        {
            return list.FindLastIndex(match);
        }

        public void ForEach(System.Action<T> action)
        {
            list.ForEach(action);
        }

        public List<T> GetRange(int index, int count)
        {
            return list.GetRange(index, count);
        }

        public int IndexOf(T item, int index)
        {
            return list.IndexOf(item, index);
        }

        public int IndexOf(T item, int index, int count)
        {
            return list.IndexOf(item, index, count);
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            list.InsertRange(index, collection);
        }

        public int LastIndexOf(T item)
        {
            return list.LastIndexOf(item);
        }

        public int LastIndexOf(T item, int index)
        {
            return list.LastIndexOf(item, index);
        }

        public int LastIndexOf(T item, int index, int count)
        {
            return list.LastIndexOf(item, index, count);
        }

        public int RemoveAll(Predicate<T> match)
        {
            return list.RemoveAll(match);
        }

        public void RemoveRange(int index, int count)
        {
            list.RemoveRange(index, count);
        }

        public void Reverse()
        {
            list.Reverse();
        }

        public void Reverse(int index, int count)
        {
            list.Reverse(index, count);
        }

        public void Sort()
        {
            list.Sort();
        }

        public void Sort(IComparer<T> comparer)
        {
            list.Sort(comparer);
        }

        public void Sort(Comparison<T> comparison)
        {
            list.Sort(comparison);
        }

        public void Sort(int index, int count, IComparer<T> comparer)
        {
            list.Sort(index, count, comparer);
        }

        public T[] ToArray()
        {
            return list.ToArray();
        }

        public void TrimExcess()
        {
            list.TrimExcess();
        }

        public bool TrueForAll(Predicate<T> match)
        {
            return list.TrueForAll(match);
        }
    }
}
