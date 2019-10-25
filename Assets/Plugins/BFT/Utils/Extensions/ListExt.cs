using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BFT
{
    public static class ListExt
    {
        public static object Random(this Array array)
        {
            int i = UnityEngine.Random.Range(0, array.Length);
            return array.GetValue(i);
        }

        public static int LoopID<T>(this List<T> list, int toLoop)
        {
            if (list.Count == 0)
            {
                return -1;
            }


            if (toLoop >= list.Count)
            {
                return toLoop % list.Count;
            }

            if (toLoop < 0)
            {
                toLoop = toLoop % list.Count;
                if (toLoop != 0)
                    toLoop = list.Count + toLoop;

                return toLoop;
            }

            return toLoop;
        }

        public static bool IsEmpty<T>(this List<T> list)
        {
            return (list.Count == 0);
        }

        public static void RemoveNulls<T>(this List<T> list) where T : UnityEngine.Object
        {
            list.RemoveAll(_ => !_);
        }

        public static List<T> RemoveReturn<T>(this List<T> list, T toRemove)
        {
            list.Remove(toRemove);
            return list;
        }

        public static List<T> AddReturn<T>(this List<T> list, T toAdd)
        {
            list.Add(toAdd);
            return list;
        }

        public static List<T> AddRangeReturn<T>(this List<T> list, IEnumerable<T> toAdd)
        {
            list.AddRange(toAdd);
            return list;
        }

        public static List<T> AddRangeReturn<T>(this List<T> list, T[] toAdd)
        {
            list.AddRange(toAdd);
            return list;
        }

        public static bool IsEmpty<T>(this IList<T> lst)
        {
            return lst.Count == 0;
        }

        [Obsolete("Use Random() instead")]
        public static T GetRandom<T>(this IEnumerable<T> me)
        {
            return Random(me);
        }

        public static bool ContainsAll<T>(this IEnumerable<T> me, IEnumerable<T> other)
        {
            var enumerable = me as T[] ?? me.ToArray();

            foreach (T member in other)
            {
                if (!enumerable.Contains(member))
                    return false;
            }

            return true;
        }

        public static T Random<T>(this IEnumerable<T> me)
        {
            var enumerable = me as IList<T> ?? me.ToList();
            return enumerable.ElementAt(UnityEngine.Random.Range(0, enumerable.Count()));
        }

        public static T GetWeightedRandom<T>(this IList<T> from, IList<float> weights, float totalWeight)
        {
            UnityEngine.Debug.Assert(from.Count == weights.Count, "The Random element cannot be retrieved" +
                                                      " because the count of weights and " +
                                                      "elements isn't the same");
            float f = UnityEngine.Random.Range(0, totalWeight);
            float cumulative = weights[0];

            for (int i = 0; i < from.Count; i++)
            {
                if (Mathf.Approximately(weights[i], 0))
                    continue;

                if (f > cumulative)
                {
                    cumulative += weights[i];
                }
                else
                {
                    return from[i];
                }
            }

            //shouldn't happen
            return from.Last();
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;

            while (n > 1)
            {
                n--;
                var k = UnityEngine.Random.Range(0, n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void Shuffle<T>(this IList<T> list, int fromIndex, int count)
        {
            if (count <= 0)
                throw new ArgumentException("Count should be positive number!");
            if (fromIndex < 0 || fromIndex >= list.Count)
                throw new IndexOutOfRangeException();
            if (fromIndex + count > list.Count)
                throw new IndexOutOfRangeException();

            var elementsLeft = count;
            var k = UnityEngine.Random.Range(fromIndex, fromIndex + elementsLeft);
            while (elementsLeft >= 1)
            {
                elementsLeft--;
                var value = list[k];
                list[k] = list[elementsLeft + fromIndex];
                list[elementsLeft + fromIndex] = value;
            }
        }

        public static void ForEach<T>(this IEnumerable<T> ie, System.Action<T> action)
        {
            foreach (var i in ie)
            {
                action(i);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> ie, System.Action<T, int> action)
        {
            int i = 0;
            foreach (var e in ie)
            {
                action(e, i);
                i++;
            }
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T> en, IEnumerable<T> toAppend)
        {
            List<T> appened = en.ToList();
            appened.AddRange(toAppend);
            return appened;
        }

        public static IEnumerable<T> Convert<T, T1>(this IEnumerable<T1> en, Func<T1, T> conversion)
        {
            List<T> newList = new List<T>();

            foreach (var member in en)
            {
                newList.Add(conversion(member));
            }

            return newList;
        }

        public static int NextAvailableID(this IEnumerable<int> ids)
        {
            return Enumerable.Range(0, int.MaxValue).Except(ids).FirstOrDefault();
        }

        public static bool ContainsAny<T>(this IEnumerable<T> source, IEnumerable<T> compare)
        {
            var enumerable = compare as T[] ?? compare.ToArray();
            foreach (var t in source)
            {
                if (enumerable.Contains(t))
                    return true;
            }

            return false;
        }

        public static List<int> AllIndexesOf(this string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            List<int> indexes = new List<int>();
            for (int index = 0;; index += value.Length)
            {
                index = str.IndexOf(value, index, StringComparison.Ordinal);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }

        public static T ElementWithMin<T>(this List<T> source, Func<T, float> converter)
        {
            float min = float.MaxValue;
            int mindID = -1;
            for (int i = 0; i < source.Count; i++)
            {
                float value = converter(source[i]);
                if (value < min)
                {
                    mindID = i;
                    min = value;
                }
            }

            if (mindID > 0)
                return source[mindID];
            return default;
        }

        public static T ElementWithMax<T>(this List<T> source, Func<T, float> converter)
        {
            float max = float.MinValue;
            int maxID = -1;
            for (int i = 0; i < source.Count; i++)
            {
                float value = converter(source[i]);
                if (value > max)
                {
                    maxID = i;
                    max = value;
                }
            }

            if (maxID > 0)
                return source[maxID];
            return default;
        }

        public static Dictionary<int, T> ListToDictionaryWithID<T>(this List<T> list)
        {
            Dictionary<int, T> dic = new Dictionary<int, T>();

            for (int i = 0; i < list.Count; i++)
            {
                dic.Add(i, list[i]);
            }

            return dic;
        }
    }
}
