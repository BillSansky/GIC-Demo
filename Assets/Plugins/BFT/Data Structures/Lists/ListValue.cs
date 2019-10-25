using System.Collections.Generic;

namespace BFT
{
    public class ListValue<T> : GenericValue<List<T>>
    {
        public int Count => Value.Count;

        public T this[int key] => Value[key];
    }
}
