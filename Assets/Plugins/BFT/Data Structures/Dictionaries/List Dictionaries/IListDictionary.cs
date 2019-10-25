using System.Collections.Generic;

namespace BFT
{
    public interface IListDictionary<T, T1> : IEnumerable<KeyValuePair<T, List<T1>>>
    {
        List<T1> this[T key] { get; set; }

        void Add(T key, T1 value);
        void Remove(T key, T1 value);

        bool ContainsKey(T key);
    }
}
