using UnityEngine;

namespace BFT
{
    public abstract class FunctionValueComponent<T> : MonoBehaviour, IValue<T>
    {
        public abstract Function<T> Function { get; }
        public T Value => Function.Value;
    }
}
