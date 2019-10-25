using System;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace BFT
{
    public class OnConditionObjectFunctionUnityEventRaiser : SerializedMonoBehaviour
    {
        public bool CheckOnEnable = false;

        public Func<UnityEngine.Object, bool> ConditionFunction;

        public UnityEngine.Object ObjectToCheck;
        public UnityEvent OnConditionMet;

        public UnityEvent OnConditionNotMet;
        public bool RaiseEventOnGameObjectActiveOnly = true;
        public bool RaiseEventOnTrue = true;

        [Button(ButtonSizes.Medium)]
        public void RaiseEventIfConditionMet()
        {
            if (RaiseEventOnGameObjectActiveOnly && !gameObject.activeInHierarchy)
                return;

            bool condition = ConditionFunction(ObjectToCheck);
            if ((condition && RaiseEventOnTrue) || (!condition && !RaiseEventOnTrue))
            {
                OnConditionMet.Invoke();
            }
            else
            {
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
