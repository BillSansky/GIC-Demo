using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    [DefaultExecutionOrder(100)]
    public class OnEnableEvent : MonoBehaviour
    {
        [InfoBox("This Event is mean to execute last: all the other objects to enabled should be enabled by now. " +
                 "If it's not the case, check the script execution orders")]
        [SerializeField]
        private UnityEvent OnEnabled;

        private void OnEnable()
        {
            OnEnabled.Invoke();
        }
    }
}
