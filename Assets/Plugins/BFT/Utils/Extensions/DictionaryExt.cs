using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BFT
{
    public static class DictionaryExt
    {
        public static void RemoveValueWhere<T, T1>(this Dictionary<T, T1> dic,
            Func<T1, bool> condition)
        {
            List<T> idsToRemove = new List<T>();

            foreach (var pair in dic)
            {
                if (condition(pair.Value))
                {
                    idsToRemove.Add(pair.Key);
                }
            }

            dic.RemoveAll(idsToRemove);
        }

        public static void RemoveAll<T, T1>(this Dictionary<T, T1> dic, IEnumerable<T> keys)
        {
            foreach (var key in keys)
            {
                dic.Remove(key);
            }
        }

        public static T RandomWeightedElement<T>(this IDictionary<T, float> elementsByWeight, float totalWeight)
        {
            float f = Random.Range(0, totalWeight);
            float cumulative = 0;

            T lastKey = default(T);

            foreach (var key in elementsByWeight.Keys)
            {
                if (Mathf.Approximately(elementsByWeight[key], 0))
                    continue;

                if (f < cumulative)
                {
                    return lastKey;
                }

                lastKey = key;
                cumulative += elementsByWeight[key];
            }

            return lastKey;
        }

        public static void AddOrReplace<T, T1>(this IDictionary<T, T1> dic, T key, T1 value)
        {
            if (dic.ContainsKey(key))
                dic[key] = value;
            else
            {
                dic.Add(key, value);
            }
        }

        public static int AddOnFirstAvailableID<T>(this IDictionary<int, T> dic, T data)
        {
            int firstAvailable = Enumerable.Range(0, int.MaxValue).Except(dic.Keys).FirstOrDefault();

            if (!dic.ContainsKey(firstAvailable))
            {
                dic.Add(firstAvailable, data);

                return firstAvailable;
            }

            return -1;
        }

        public static int AddOnFirstAvailableID<T>(this IDictionary<int, T> dic, ICollection<int> excludedKeys, T data)
        {
            int firstAvailable = Enumerable.Range(0, int.MaxValue).Except(dic.Keys).Except(excludedKeys).FirstOrDefault();

            if (!dic.ContainsKey(firstAvailable))
            {
                dic.Add(firstAvailable, data);

                return firstAvailable;
            }

            return -1;
        }

        public static void AddToList<T, T1>(this Dictionary<T, List<T1>> listDic, T key, T1 value)
        {
            if (!listDic.ContainsKey(key))
            {
                listDic[key] = new List<T1>();
            }

            listDic[key].Add(value);
        }

        public static void AddToHash<T, T1>(this Dictionary<T, HashSet<T1>> listDic, T key, T1 value)
        {
            if (!listDic.ContainsKey(key))
            {
                listDic[key] = new HashSet<T1>();
            }

            listDic[key].Add(value);
        }

        public static Dictionary<T, T1> ConvertValues<T, T1, T3>(this Dictionary<T, T3> dic, Func<T3, T1> func)
        {
            var newDic = new Dictionary<T, T1>(dic.Count);

            foreach (var pair in dic)
            {
                newDic.Add(pair.Key, func(pair.Value));
            }

            return newDic;
        }

        public static T RandomValue<T1, T>(this Dictionary<T1, T> dic)
        {
            var randKey = dic.Keys.Random();
            return dic[randKey];
        }

        public static Dictionary<T, List<T1>> ConvertValues<T, T1, T3>(this Dictionary<T, List<T3>> dic, Func<T3, T1> func)
        {
            var newDic = new Dictionary<T, List<T1>>(dic.Count);

            foreach (var pair in dic)
            {
                newDic.Add(pair.Key, (List<T1>) pair.Value.Convert(func));
            }

            return newDic;
        }

        public static void IncrementOrAdd<T>(this Dictionary<T, int> dic, T key, int value)
        {
            if (dic.ContainsKey(key))
            {
                dic[key] += value;
            }
            else
            {
                dic.Add(key, value);
            }
        }

        public static void DecrementOrAdd<T>(this Dictionary<T, int> dic, T key, int value)
        {
            if (dic.ContainsKey(key))
            {
                dic[key] -= value;
            }
            else
            {
                dic.Add(key, -value);
            }
        }
        
    }
}
