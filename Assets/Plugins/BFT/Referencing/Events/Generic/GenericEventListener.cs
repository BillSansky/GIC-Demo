using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    public abstract class GenericEventListener<T, T1> : MonoBehaviour, IGenericEventListener<T> where T1 : UnityEvent<T>
    {
        [BoxGroup("Events")] public T1 Response;

        [BoxGroup("Events"), HideIf("UseInNestedPrefab")]
        public abstract GenericEventAsset<T, T1> Event { get; }

        public void OnEventRaised(T param)
        {
            if (!enabled)
                return;
            Response.Invoke(param);
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
        public void RaiseEvent(T param)
        {
            OnEventRaised(param);
        }

        [FoldoutGroup("Tools"), Button("Raise Event Response", ButtonSizes.Medium)]
        public void RaiseEvent()
        {
            OnEventRaised(default);
        }
    }
}
