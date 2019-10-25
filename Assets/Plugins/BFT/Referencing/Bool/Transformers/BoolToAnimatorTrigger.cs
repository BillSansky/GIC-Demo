using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class BoolToAnimatorTrigger : SerializedMonoBehaviour
    {
        [BoxGroup("Animator")] public Animator Animator;

        [BoxGroup("Condition")] public bool AutoUpdate;

        [BoxGroup("Condition")] public IValue<bool> Condition;

        [BoxGroup("Animator")] public string ConditionName;

        public void SetCondition()
        {
            if (Condition.Value)
                Animator.SetTrigger(ConditionName);
        }

        public void OnEnable()
        {
            SetCondition();
            if (AutoUpdate)
                StartCoroutine(CheckCondition());
        }

        public void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator CheckCondition()
        {
            yield return null;
            SetCondition();
        }
    }
}
