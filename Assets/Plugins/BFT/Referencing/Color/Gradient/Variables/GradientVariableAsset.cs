using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    public class GradientEvent : UnityEvent<UnityEngine.Gradient>
    {
    }

    public class GradientVariableAsset : SerializedScriptableObject, IVariable<UnityEngine.Gradient>
    {
        [SerializeField] private UnityEngine.Gradient value;

        public UnityEngine.Gradient Value
        {
            get => value;
            set => this.value = value;
        }
    }
}
