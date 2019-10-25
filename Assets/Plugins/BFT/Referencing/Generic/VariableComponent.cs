using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    /// <summary>
    ///     A component holding a value that can be changed, or a reference to one
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="T1"></typeparam>
    public abstract class VariableComponent<T> : MonoBehaviour, IVariable<T>
    {
        [Tooltip("If true, the value will emit an event every time it is changed. " +
                 "Otherwise, no event will ever be fired." +
                 " Put false if the value changes every frame to avoid garbage collection")]
        [BoxGroup("Events", Order = 999999), SerializeField]
        private bool emitUnityEvent;

        [InfoBox("The Event will be called only when this component sets the value, " +
                 "but not when the value it references changes.", InfoMessageType.Info, "emitUnityEvent")]
        [BoxGroup("Events"), SerializeField, ShowIf("emitUnityEvent")]
        private UnityEvent onValueChanged;

        [HideReferenceObjectPicker] public abstract GenericVariable<T> Variable { get; }

        public bool IsVariableReference => Variable.UseReference;

        private bool VariableConstantAndEmitEvent => IsVariableReference && emitUnityEvent;

        public T Value
        {
            get => Variable.Value;

            set
            {
                Variable.Value = value;

                if (emitUnityEvent)
                {
                    EmitEvent();
                }
            }
        }

        protected virtual void EmitEvent()
        {
            onValueChanged.Invoke();
        }
    }
}
