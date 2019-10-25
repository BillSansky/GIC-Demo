using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    public class EventListener : MonoBehaviour, IEventListener
    {
        [FoldoutGroup("Tools")] public bool DebugLog;

        [BoxGroup("Events"), AssetSelector(Filter = "t:EventAsset")]
        public EventAsset Event;

        [BoxGroup("Events")] public UnityEvent Response;

        public void OnEventRaised(IEvent evt)
        {
            if (!enabled)
                return;

            if (DebugLog)
                UnityEngine.Debug.LogFormat(this, "Event Response received on {0}", name);
            Response.Invoke();
        }


        public void OnEnable()
        {
            Event.RegisterListener(this);
        }

        public void OnDisable()
        {
            Event.UnRegisterListener(this);
        }

        [FoldoutGroup("Tools"), Button("Raise Event Response", ButtonSizes.Medium)]
        public void RaiseEvent()
        {
            OnEventRaised(null);
        }
    }
}
