using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    public class UnityEventComponent : MonoBehaviour, IValue<UnityEvent>
    {
        public UnityEvent Event;
        public UnityEvent Value => Event;

        [Button(ButtonSizes.Medium)]
        public void Invoke()
        {
            Event.Invoke();
        }
    }
}
