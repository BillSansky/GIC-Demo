using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class AnimatorParameterTrigger : SerializedMonoBehaviour
    {
        [SerializeField, ShowInInspector] private UnityEngine.Animator animator;

        public UnityEngine.Animator Animator => animator;

        public void SetBool(string boolName, bool boolValue)
        {
            Animator.SetBool(boolName, boolValue);
        }

        public void SetBoolTrue(string boolName)
        {
            Animator.SetBool(boolName, true);
        }

        public void SetBoolFalse(string boolName)
        {
            if (Animator)
            {
                Animator.SetBool(boolName, false);
            }
        }

        public void Trigger(string triggerName)
        {
            if (Animator)
            {
                Animator.SetTrigger(triggerName);
            }
        }
    }
}
