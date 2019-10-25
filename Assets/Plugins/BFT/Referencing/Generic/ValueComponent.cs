using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    /// <summary>
    ///     A component holding a value or a reference to a value
    /// </summary>
    /// <typeparam name="T"> the value to return</typeparam>
    /// <typeparam name="T1">the value holder type</typeparam>
    public abstract class ValueComponent<T, T1> : MonoBehaviour, IValue<T> where T1 : GenericValue<T>
    {
        [SerializeField] private T1 value;

        public bool IsVariableReference => value.UseReference && value.Reference != this;

        [ShowInInspector, ShowIf("IsVariableReference")]
        public T Value => value.Value;
    }
}
