using Sirenix.OdinInspector;
using UnityEngine.Serialization;

namespace BFT
{
    public class StateMachineStateTransitionOnUpdate : SerializedMonoBehaviour
    {
        [FormerlySerializedAs("ConditionsToMeet")]
        public IValue<bool> ConditionToMeet;

        public StateMachine StateMachine;
        public StateMachineState StateToTransitionTo;

        public void Update()
        {
            if (ConditionToMeet.Value)
            {
                StateMachine.TransitionToState(StateToTransitionTo);
            }
        }
    }
}
