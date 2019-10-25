using UnityEngine.Events;

namespace BFT
{
    public abstract class ValueEvent<T, T1, T2> where T1 : GenericValue<T> where T2 : UnityEvent<T>
    {
        public T2 Event;
        public T1 Value;

        public void Invoke()
        {
            Event.Invoke(Value.Value);
        }
    }
}
