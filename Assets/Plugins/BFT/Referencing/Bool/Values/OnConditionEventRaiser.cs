using Sirenix.OdinInspector;

namespace BFT
{
    public class OnConditionEventRaiser : SerializedMonoBehaviour
    {
        [InfoBox("Can check for a boolean value before raising an event by calling 'CheckAndRaise'." +
                 "\nIn that case the event will be raised only if the bool value is true")]
        [BoxGroup("Reference")]
        public IValue<bool> Condition;

        [BoxGroup("Reference")] public IEvent Event;

        [BoxGroup("Utils"), Button(ButtonSizes.Medium)]
        public void CheckAndRaise()
        {
            if (Condition == null || Condition.Value)
                Event.RaiseEvent();
        }
    }
}
