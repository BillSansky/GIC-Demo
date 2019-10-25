using Sirenix.OdinInspector;
using UnityEngine.Events;
#if BFT_REWIRED
using UnityEngine;

namespace BFT
{
    public class ButtonActionUnityEvent : ButtonAction
    {
        [BoxGroup("Options")] public bool DebugLog;

        [BoxGroup("Events")] public UnityEvent OnButtonDown, OnButtonUp;

        public override void OnButtonDownAction()
        {
            if (!gameObject.activeInHierarchy)
                return;

            if (DebugLog)
            {
                UnityEngine.Debug.Log("Button Down");
            }

            OnButtonDown.Invoke();
        }

        public override void OnButtonHeldAction()
        {
        }

        public override void OnButtonUpAction()
        {
            if (!gameObject.activeInHierarchy)
                return;

            if (DebugLog)
            {
                UnityEngine.Debug.Log("Button UP");
            }

            OnButtonUp.Invoke();
        }
    }
}
#endif
