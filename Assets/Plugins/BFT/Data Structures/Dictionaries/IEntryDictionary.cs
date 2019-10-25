using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BFT
{
    /// <inheritdoc />
    /// <summary>
    ///     Holds a set of content identified by an int ID so that it can be easily linked and used, and can set content
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntryDictionary<T> : IReadOnlyDictionary<T>, IEnumerable<int>
    {
        new T this[int index] { get; set; }

        void Set(int id, T value);
        int Add(T value);
        void Remove(int id);
    }

    public interface IReadOnlyDictionary<out T> : IIDsHolder
    {
        T this[int index] { get; }
        new ValueDropdownList<int> ContentIDs { get; }
        IEnumerable<T> Values { get; }
        T Get(int id);
    }

    /// <summary>
    ///     Used as an object that holds references to ID without knowing about the data
    /// </summary>
    public interface IIDsHolder
    {
        ValueDropdownList<int> ContentIDs { get; }

        IEnumerable<int> Keys { get; }
        bool ContainsKey(int id);
    }
}
