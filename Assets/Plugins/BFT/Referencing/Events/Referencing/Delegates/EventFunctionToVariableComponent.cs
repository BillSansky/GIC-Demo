using UnityEngine.Events;

namespace BFT
{
    public class EventFunctionValueComponent : FunctionValueComponent<UnityEvent>
    {
        public EventFunction EventFunction;
        public override Function<UnityEvent> Function => EventFunction;
    }
}
