using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    public class OnConditionUnityEventRaiser : MonoBehaviour
    {
        [BoxGroup("Options")] public bool CheckOnEnable = false;
        [BoxGroup("Condition")] public BoolValue Condition = new BoolValue() {UseReference = true};

        [BoxGroup("Tools")] public bool DebugLog = false;
        public UnityEvent OnConditionMet;

        public UnityEvent OnConditionNotMet;
        [BoxGroup("Options")] public bool RaiseEventOnGameObjectActiveOnly = true;
        [BoxGroup("Condition")] public bool RaiseEventOnTrue = true;

        [Button(ButtonSizes.Medium), BoxGroup("Tools")]
        public void RaiseEventIfConditionMet()
        {
            if (RaiseEventOnGameObjectActiveOnly && !gameObject.activeInHierarchy)
                return;

            bool condition = Condition.Value;
            if ((condition && RaiseEventOnTrue) || (!condition && !RaiseEventOnTrue))
            {
                if (DebugLog)
                    UnityEngine.Debug.Log($"Condition met on {name}", this);
                OnConditionMet.Invoke();
            }
            else
            {
                if (DebugLog)
                    UnityEngine.Debug.Log($"Condition not met on {name}", this);

                OnConditionNotMet.Invoke();
            }
        }

        public void OnEnable()
        {
            if (CheckOnEnable)
                RaiseEventIfConditionMet();
        }
    }
}
