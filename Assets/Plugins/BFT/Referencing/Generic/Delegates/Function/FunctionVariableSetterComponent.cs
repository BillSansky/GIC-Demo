using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public abstract class FunctionVariableSetterComponent<T, TFunc, TVar> : MonoBehaviour, IValue<T>
        where TFunc : Function<T> where TVar : GenericVariable<T>
    {
        public TFunc Function;

        public bool SetOnAwake = true;

        [SerializeField] private bool SetRegularly = false;

        [SerializeField] [ShowIf("SetRegularly")]
        private float TimeInterval = 0;

        public TVar Variable;

        public T Value => Variable.Value;

        public void SetValue()
        {
            Variable.Value = Function.Value;
        }

        public void Awake()
        {
            if (SetOnAwake)
                SetValue();
        }

        public void OnEnable()
        {
            if (SetRegularly)
            {
                StopAllCoroutines();
                this.CallRegularly(TimeInterval, SetValue);
            }
        }
    }
}
