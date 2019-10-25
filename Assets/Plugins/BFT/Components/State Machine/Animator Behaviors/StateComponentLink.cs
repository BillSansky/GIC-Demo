using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    /// <summary>
    ///     needed to be referenced to monobehaviours
    /// </summary>
    public class StateComponentLink : SerializedStateMachineBehaviour
    {
        private AnimatorStateBehaviorComponent component;
        public int id;

        public void Reset()
        {
            id = GetHashCode();
        }

        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            base.OnStateMachineEnter(animator, stateMachinePathHash);
            GetComponent(animator);
        }

        private void GetComponent(Animator animator)
        {
            var machine = animator.gameObject.GetComponent<AnimatorStateMachineComponent>();
            if (!machine)
            {
                UnityEngine.Debug.LogWarning("The state component link could not find a " +
                                 "state machine component on its game object, make sure to link one", animator);
            }
            else
            {
                component = machine.BehaviorComponentPerState[id];
            }
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            if (!component)
                GetComponent(animator);
            if (component)
                component.OnStateEntered();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            if (!component)
                GetComponent(animator);
            if (component)
                component.OnStateExit();
        }
    }
}
