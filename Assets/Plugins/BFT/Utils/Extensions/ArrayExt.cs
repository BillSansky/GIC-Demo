using System;
using System.Collections.Generic;

namespace BFT
{
    public static class ArrayExt
    {
        public static T[] Add<T>(this T[] source, T item)
        {
            Array.Resize<T>(ref source, source.Length + 1);
            source[source.Length - 1] = item;
            return source;
        }

        public static T[] Remove<T>(this T[] source, T item)
        {
            List<T> tempList = new List<T>(source);
            tempList.Remove(item);
            return tempList.ToArray();
        }

        public static bool Contains<T>(this T[] source, T item)
        {
            foreach (var member in source)
            {
                if (Equals(member, item))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
