using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    /// <summary>
    ///     Sets the abnimation to play at a random speed
    /// </summary>
    public class RandomAnimationSpeed : SerializedStateMachineBehaviour
    {
        public FloatValue MaxSpeed;
        public FloatValue MinSpeed;

        private float previousSpeed;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            previousSpeed = animator.speed;
            animator.speed = Random.Range(MinSpeed.Value, MaxSpeed.Value);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.speed = previousSpeed;
        }
    }
}
