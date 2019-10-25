using Sirenix.OdinInspector;

namespace BFT
{
    public class StateMachineStateIDSwicth : SerializedMonoBehaviour
    {
        [BoxGroup("References")] public IValue<int> ID;

        [BoxGroup("References")] public StateMachine StateMachine;

        public void SwitchState()
        {
            StateMachine.TransitionToState(ID.Value);
        }
    }
}
