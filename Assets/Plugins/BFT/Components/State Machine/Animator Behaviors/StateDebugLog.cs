using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    [TypeInfoBox("Sends a log to the console whenever the state is entered or exited")]
    public class StateDebugLog : SerializedStateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            UnityEngine.Debug.Log("State Enter: " + name, this);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);

            UnityEngine.Debug.Log("State Exit: " + name, this);
        }
    }
}
