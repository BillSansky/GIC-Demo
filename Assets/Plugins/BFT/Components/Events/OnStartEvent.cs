using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    public class OnStartEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnStarted;

        private void Start()
        {
            OnStarted.Invoke();
        }
    }
}
