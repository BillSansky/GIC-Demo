using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class AnimatorPreventResetOnDisable : MonoBehaviour
    {
        [InfoBox("On Awake, configures the animator to not trash the state when disabled")]
        public Animator animator;

        public void Reset()
        {
            animator = GetComponent<Animator>();
        }

        public void Awake()
        {
            animator.keepAnimatorControllerStateOnDisable = true;
        }
    }
}
