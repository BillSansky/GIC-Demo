using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace BFT
{
    public class MonoBehaviourEvents : MonoBehaviour
    {
        [SerializeField] bool active = true;

        [FormerlySerializedAs("OnAwaked")] [SerializeField]
        private UnityEvent OnAwoken;

        [SerializeField] private UnityEvent OnDestroyed;
        [SerializeField] private UnityEvent OnDisabled;
        [SerializeField] private UnityEvent OnEnabled;
        [SerializeField] private UnityEvent OnStarted;

        private void Awake()
        {
            if (active)
                OnAwoken.Invoke();
        }

        private void OnEnable()
        {
            if (active)
                OnEnabled.Invoke();
        }

        private void Start()
        {
            if (active)
                OnStarted.Invoke();
        }

        private void OnDisable()
        {
            if (active)
                OnDisabled.Invoke();
        }

        private void OnDestroy()
        {
            if (active)
                OnDestroyed.Invoke();
        }
    }
}
