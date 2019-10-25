using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace BFT
{
    public abstract class ActionWithParamComponent<T, T1, T2> : MonoBehaviour where T1 : Action<T> where T2 : IValue<T>
    {
        [BoxGroup("Action")] public T1 Action;

        [BoxGroup("Options")] public bool ExecuteRegularly = false;

        [BoxGroup("Options"), ShowIf("ExecuteRegularly")]
        public float ExecutionTimeInterval = 0;

        [FormerlySerializedAs("Value")] [BoxGroup("Action")]
        public T2 ValueForAction;

        [FoldoutGroup("Tools")]
        [Button(ButtonSizes.Medium)]
        public void Invoke()
        {
            Action.Act(ValueForAction.Value);
        }

        public void OnEnable()
        {
            if (ExecuteRegularly)
            {
                this.CallRegularly(ExecutionTimeInterval, Invoke);
            }
        }
    }
}
