using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    public class EventToggle : MonoBehaviour
    {
        [BoxGroup("Options")] public bool FirstToggleTrue;
        [BoxGroup("Events")] public UnityEvent OnToggleFalse;

        [BoxGroup("Events")] public UnityEvent OnToggleTrue;

        [BoxGroup("Status"), ReadOnly, ShowInInspector]
        private bool toggleStatus = false;

        public void Awake()
        {
            if (!FirstToggleTrue)
                toggleStatus = true;
        }

        public void Toggle()
        {
            toggleStatus = !toggleStatus;
            if (toggleStatus)
                OnToggleTrue.Invoke();
            else
            {
                OnToggleFalse.Invoke();
            }
        }

        public void ToggleTrue()
        {
            toggleStatus = true;
            OnToggleTrue.Invoke();
        }

        public void ToggleFalse()
        {
            toggleStatus = false;
            OnToggleFalse.Invoke();
        }
    }
}
