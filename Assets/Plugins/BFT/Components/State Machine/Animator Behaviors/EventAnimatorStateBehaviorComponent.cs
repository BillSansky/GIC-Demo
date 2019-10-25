using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace BFT
{
    public class EventAnimatorStateBehaviorComponent : AnimatorStateBehaviorComponent
    {
        [BoxGroup("Utils")] public bool DebugLog;
        [BoxGroup("Events")] public UnityEvent OnStateEnteredEvent;
        [BoxGroup("Events")] public UnityEvent OnStateExitEvent;

        public override void OnStateEntered()
        {
            if (!enabled)
                return;
            if (DebugLog)
                UnityEngine.Debug.Log("State Entered", this);
            OnStateEnteredEvent.Invoke();
        }

        public override void OnStateExit()
        {
            if (!enabled)
                return;

            if (DebugLog)
                UnityEngine.Debug.Log("State Exited", this);

            OnStateExitEvent.Invoke();
        }
    }
}
