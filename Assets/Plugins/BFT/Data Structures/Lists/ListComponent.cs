using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace BFT
{
    public class ListComponent<T> : MonoBehaviour, IValue<List<T>>
    {
        public List<T> list;

        public bool IsSynchronized => ((ICollection) Value).IsSynchronized;

        public object SyncRoot => ((ICollection) Value).SyncRoot;

        public bool IsFixedSize => ((IList) Value).IsFixedSize;

        public int Capacity
        {
            get => Value.Capacity;

            set => Value.Capacity = value;
        }

        public int Count => Value.Count;

        public T this[int index]
        {
            get => Value[index];

            set => Value[index] = value;
        }

        public bool IsReadOnly => false;

        public List<T> Value => list;

        public void CopyTo(Array array, int index)
        {
            ((ICollection) Value).CopyTo(array, index);
        }

        public int Add(object value)
        {
            return ((IList) Value).Add(value);
        }

        public bool Contains(object value)
        {
            return ((IList) Value).Contains(value);
        }

        public int IndexOf(object value)
        {
            return ((IList) Value).IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            ((IList) Value).Insert(index, value);
        }

        public void Remove(object value)
        {
            ((IList) Value).Remove(value);
        }

        public void Add(T item)
        {
            Value.Add(item);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            Value.AddRange(collection);
        }

        public ReadOnlyCollection<T> AsReadOnly()
        {
            return Value.AsReadOnly();
        }

        public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
        {
            return Value.BinarySearch(index, count, item, comparer);
        }

        public int BinarySearch(T item)
        {
            return Value.BinarySearch(item);
        }

        public int BinarySearch(T item, IComparer<T> comparer)
        {
            return Value.BinarySearch(item, comparer);
        }

        public void Clear()
        {
            Value.Clear();
        }

        public bool Contains(T item)
        {
            return Value.Contains(item);
        }

        public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
        {
            return Value.ConvertAll(converter);
        }

        public void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            Value.CopyTo(index, array, arrayIndex, count);
        }

        public void CopyTo(T[] array)
        {
            Value.CopyTo(array);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Value.CopyTo(array, arrayIndex);
        }

        public bool Exists(Predicate<T> match)
        {
            return Value.Exists(match);
        }

        public T Find(Predicate<T> match)
        {
            return Value.Find(match);
        }

        public List<T> FindAll(Predicate<T> match)
        {
            return Value.FindAll(match);
        }

        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            return Value.FindIndex(startIndex, count, match);
        }

        public int FindIndex(int startIndex, Predicate<T> match)
        {
            return Value.FindIndex(startIndex, match);
        }

        public int FindIndex(Predicate<T> match)
        {
            return Value.FindIndex(match);
        }

        public T FindLast(Predicate<T> match)
        {
            return Value.FindLast(match);
        }

        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            return Value.FindLastIndex(startIndex, count, match);
        }

        public int FindLastIndex(int startIndex, Predicate<T> match)
        {
            return Value.FindLastIndex(startIndex, match);
        }

        public int FindLastIndex(Predicate<T> match)
        {
            return Value.FindLastIndex(match);
        }

        public void ForEach(System.Action<T> action)
        {
            Value.ForEach(action);
        }

        public List<T> GetRange(int index, int count)
        {
            return Value.GetRange(index, count);
        }

        public int IndexOf(T item)
        {
            return Value.IndexOf(item);
        }

        public int IndexOf(T item, int index)
        {
            return Value.IndexOf(item, index);
        }

        public int IndexOf(T item, int index, int count)
        {
            return Value.IndexOf(item, index, count);
        }

        public void Insert(int index, T item)
        {
            Value.Insert(index, item);
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            Value.InsertRange(index, collection);
        }

        public int LastIndexOf(T item)
        {
            return Value.LastIndexOf(item);
        }

        public int LastIndexOf(T item, int index)
        {
            return Value.LastIndexOf(item, index);
        }

        public int LastIndexOf(T item, int index, int count)
        {
            return Value.LastIndexOf(item, index, count);
        }

        public bool Remove(T item)
        {
            return Value.Remove(item);
        }

        public int RemoveAll(Predicate<T> match)
        {
            return Value.RemoveAll(match);
        }

        public void RemoveAt(int index)
        {
            Value.RemoveAt(index);
        }

        public void RemoveRange(int index, int count)
        {
            Value.RemoveRange(index, count);
        }

        public void Reverse()
        {
            Value.Reverse();
        }

        public void Reverse(int index, int count)
        {
            Value.Reverse(index, count);
        }

        public void Sort()
        {
            Value.Sort();
        }

        public void Sort(IComparer<T> comparer)
        {
            Value.Sort(comparer);
        }

        public void Sort(Comparison<T> comparison)
        {
            Value.Sort(comparison);
        }

        public void Sort(int index, int count, IComparer<T> comparer)
        {
            Value.Sort(index, count, comparer);
        }

        public T[] ToArray()
        {
            return Value.ToArray();
        }

        public void TrimExcess()
        {
            Value.TrimExcess();
        }

        public bool TrueForAll(Predicate<T> match)
        {
            return Value.TrueForAll(match);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }
}
