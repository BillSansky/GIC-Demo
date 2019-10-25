using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    /// <summary>
    ///     A component that can serialize reference to interface types
    /// </summary>
    /// <typeparam name="T"> the type of value to return</typeparam>
    /// <typeparam name="T1">the value type that will be casted to the interface</typeparam>
    /// <typeparam name="T2">The differential value  type</typeparam>
    public abstract class DifferentialValueComponent<T, T1, T2> : MonoBehaviour, IValue<T>
        where T2 : GenericDifferentialValue<T, T1> where T1 : T
    {
        [SerializeField] private T2 value;

        public bool IsVariableReference => value.UseReference;

        [ShowInInspector, ShowIf("IsVariableReference")]
        public T Value => value.Value;
    }
}
