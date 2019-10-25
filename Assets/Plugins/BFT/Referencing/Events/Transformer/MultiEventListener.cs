using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    public class MultiEventListener : SerializedMonoBehaviour, IEventListener
    {
        private int eventLeftToRaise = 0;

        [SerializeField] private List<IEvent> eventsToWatch = new List<IEvent>();

        public UnityEvent OnAllEventInvoked;

        public bool RestartTriggerOnRaised;

        public void OnEventRaised(IEvent eventWatched)
        {
            eventLeftToRaise--;

            if (eventLeftToRaise == 0)
            {
                if (RestartTriggerOnRaised)
                    eventLeftToRaise = eventsToWatch.Count;

                OnAllEventInvoked.Invoke();
            }
        }

        public void OnEnable()
        {
            foreach (var evt in eventsToWatch)
            {
                evt.RegisterListener(this);
            }

            eventLeftToRaise = eventsToWatch.Count;
        }

        public void OnDisable()
        {
            foreach (var evt in eventsToWatch)
            {
                evt.UnRegisterListener(this);
            }

            eventLeftToRaise = 0;
        }
    }
}
