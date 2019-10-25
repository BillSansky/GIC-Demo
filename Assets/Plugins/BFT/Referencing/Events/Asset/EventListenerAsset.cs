using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    [CreateAssetMenu(menuName = "BFT/Data/Event/Event Listener", fileName = "Event Listener")]
    public class EventListenerAsset : SerializedScriptableObject, IEventListener
    {
        [BoxGroup("")] public IEvent Event;

        [BoxGroup("Events")] public UnityEvent Response;

        public void OnEventRaised(IEvent eventWatched)
        {
            Response.Invoke();
        }

        public void Reset()
        {
            AddListenerToManager();
        }

        [FoldoutGroup("Utils"), Button(ButtonSizes.Medium)]
        private void AddListenerToManager()
        {
            EventListenerAssetManager.Instance.AddListener(this);
        }

        public void OnEnable()
        {
            RegisterListener();
        }

        public void RegisterListener()
        {
            Event.RegisterListener(this);
        }

        public void OnDisable()
        {
            Event.UnRegisterListener(this);
        }
    }
}
