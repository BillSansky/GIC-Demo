using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    [CreateAssetMenu(menuName = "BFT/Data/Event/Event Invoker", fileName = "Event Invoker")]
    public class EventAsset : ScriptableObject, IEvent
    {
        [BoxGroup("Status")] [ShowInInspector] private readonly List<IEventListener> listeners = new List<IEventListener>();

        private readonly List<IEventListener> listenersToRemoveDuringRaising = new List<IEventListener>(2);

        [BoxGroup("Utils")] public bool DebugLog;

        [BoxGroup("Events")] public UnityEvent OnBeforeEventRaised;

        [BoxGroup("Events")] public UnityEvent OnEventRaised;

        [NonSerialized] private bool raising = false;

        [BoxGroup("Utils")]
        [Button(ButtonSizes.Medium)]
        public void RaiseEvent()
        {
            if (DebugLog)
                UnityEngine.Debug.Log($"Event {name} Raised, with {listeners.Count} listeners active ", this);

            OnBeforeEventRaised.Invoke();

            raising = true;

            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised(this);
            }

            raising = false;

            if (listenersToRemoveDuringRaising.Count > 0)
            {
                for (int i = 0; i < listenersToRemoveDuringRaising.Count; i++)
                {
                    listeners.Remove(listenersToRemoveDuringRaising[i]);
                }

                listenersToRemoveDuringRaising.Clear();
            }

            OnEventRaised.Invoke();
        }

        public void RegisterListener(IEventListener listener)
        {
            if (!listeners.Contains(listener))
            {
                listeners.Add(listener);
                if (raising)
                    listener.OnEventRaised(this);
            }
        }

        public void UnRegisterListener(IEventListener listener)
        {
            if (raising)
                listenersToRemoveDuringRaising.Add(listener);
            else
                listeners.Remove(listener);
        }
    }
}
