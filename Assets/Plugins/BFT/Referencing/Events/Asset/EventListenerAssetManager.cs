using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace BFT
{
    public class EventListenerAssetManager : SingletonSO<EventListenerAssetManager>
    {
        [SerializeField, ReadOnly] private List<EventListenerAsset> listeners = new List<EventListenerAsset>();

        public void AddListener(EventListenerAsset listenerAsset)
        {
            if (listeners.Contains(listenerAsset))
                return;
            listeners.Add(listenerAsset);
        }

        public void Awake()
        {
            foreach (EventListenerAsset listener in listeners)
            {
                listener.RegisterListener();
            }
        }
    }
}
