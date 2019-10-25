using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    public class EventLink : SerializedMonoBehaviour, IValue<UnityEvent>
    {
        public UnityEvent delegateEvent;
        [SerializeField] private IValue<UnityEvent> referenceEvent;

        public UnityEvent Value => referenceEvent.Value;

        public void OnEnable()
        {
            referenceEvent.Value.AddListener(delegateEvent.Invoke);
        }

        public void OnDisable()
        {
            referenceEvent.Value.RemoveListener(delegateEvent.Invoke);
        }
    }
}
