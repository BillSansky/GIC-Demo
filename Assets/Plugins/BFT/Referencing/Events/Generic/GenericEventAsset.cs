using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace BFT
{
    public abstract class GenericEventAsset<T, T2> : SerializedScriptableObject, IGenericEvent<T> where T2 : UnityEvent<T>
    {
        [BoxGroup("Status")] [ShowInInspector, ReadOnly]
        private readonly List<IGenericEventListener<T>> listeners = new List<IGenericEventListener<T>>();

        private readonly List<IGenericEventListener<T>> listenersToRemoveDuringRaising =
            new List<IGenericEventListener<T>>(2);

        private readonly List<IEventListener> simpleListeners = new List<IEventListener>();

        private readonly List<IEventListener> simpleListenersToRemoveDuringRaising = new List<IEventListener>(2);

        [BoxGroup("Utils")] public bool DebugLog;

        [BoxGroup("Events")] public T2 OnBeforeEventRaised;

        [BoxGroup("Events")] public T2 OnEventRaised;

        private T param;

        private bool raising = false;

        [FoldoutGroup("Tools"), Button(ButtonSizes.Medium)]
        public void RaiseEvent(T param)
        {
            if (DebugLog)
            {
                UnityEngine.Debug.LogFormat(this, "Event {0} Raised", this.name);
            }

            OnBeforeEventRaised.Invoke(param);

            raising = true;
            this.param = param;

            for (int i = simpleListeners.Count - 1; i >= 0; i--)
            {
                simpleListeners[i].OnEventRaised(this);
            }

            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised(param);
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

            if (simpleListenersToRemoveDuringRaising.Count > 0)
            {
                for (int i = 0; i < simpleListenersToRemoveDuringRaising.Count; i++)
                {
                    simpleListeners.Remove(simpleListenersToRemoveDuringRaising[i]);
                }

                simpleListenersToRemoveDuringRaising.Clear();
            }

            OnEventRaised.Invoke(param);
        }

        public void RegisterListener(IGenericEventListener<T> listener)
        {
            if (!listeners.Contains(listener))
            {
                listeners.Add(listener);
                if (raising)
                {
                    listener.OnEventRaised(param);
                }
            }
        }

        public void UnRegisterListener(IGenericEventListener<T> listener)
        {
            if (raising)
            {
                listenersToRemoveDuringRaising.Add(listener);
            }
            else
            {
                listeners.Remove(listener);
            }
        }

        [FoldoutGroup("Tools"), Button(ButtonSizes.Medium)]
        public void RaiseEvent()
        {
            RaiseEvent(default);
        }

        public void RegisterListener(IEventListener listener)
        {
            if (!simpleListeners.Contains(listener))
            {
                simpleListeners.Add(listener);
                if (raising)
                {
                    listener.OnEventRaised(this);
                }
            }
        }

        public void UnRegisterListener(IEventListener listener)
        {
            if (raising)
            {
                simpleListenersToRemoveDuringRaising.Add(listener);
            }
            else
            {
                simpleListeners.Remove(listener);
            }
        }

        void OnEnable()
        {
            raising = false;
        }
    }
}
