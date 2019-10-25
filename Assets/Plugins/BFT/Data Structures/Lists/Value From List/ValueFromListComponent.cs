using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    /// <summary>
    ///     Gets a value from a list given an index
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public class ValueFromListComponent<T, T1, T2> : MonoBehaviour, IValue<T>
        where T1 : ValueFromList<T, T2> where T2 : IValue<List<T>>
    {
        [BoxGroup("Reference")] public T1 List;

        public T Value => List.ListValue;
    }
}
