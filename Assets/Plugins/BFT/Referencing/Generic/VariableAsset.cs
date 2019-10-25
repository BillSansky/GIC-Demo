using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    public enum VariableChangePermission
    {
        FORBIDDEN,
        ALLOW,
        ALLOW_TEMPORARY
    }

    /// <summary>
    ///     An Asset Holding a value that can be changed or not depending on the authorization it gets
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    [Serializable]
    public class VariableAsset<T1> : SerializedScriptableObject, IVariable<T1>
    {
        [SerializeField, HideInInspector] private T1 cachedValue;

        [Tooltip("If true, the value will emit an event every time it is changed. " +
                 "Otherwise, no event will ever be fired." +
                 " Put false if the value changes every frame to avoid garbage collection")]
        [BoxGroup("Events"), SerializeField]
        private bool emitUnityEvent;

        [BoxGroup("Options"), SerializeField] private bool logDebug;

        [BoxGroup("Events"), SerializeField, ShowIf("emitUnityEvent")]
        public UnityEvent OnValueChanged;

        [BoxGroup("Variable",Order = -1), SerializeField, OnValueChanged("ChangeCachedValue"), HideLabel]
        private T1 value;

        [BoxGroup("Options"), SerializeField]
        private VariableChangePermission variableChangePermission = VariableChangePermission.ALLOW;

        public bool IsVariableChangeDiscrete
        {
            get => emitUnityEvent;
            set => emitUnityEvent = value;
        }

        public VariableChangePermission ChangePermission
        {
            get => variableChangePermission;
            set => variableChangePermission = value;
        }

        public virtual T1 Value
        {
            get => value;
            set
            {
                if (logDebug)
                {
                    UnityEngine.Debug.LogFormat(this, "Value change was requested on {0}", name);
                }

                if (ChangePermission == VariableChangePermission.FORBIDDEN)
                {
                    UnityEngine.Debug.LogError("BFT Error: you are trying to change the value of a variable " +
                                   "that was marked FORBIDDEN");
                    return;
                }

                if (!Equals(value, this.value))
                {
                    this.value = value;
                    if (emitUnityEvent)
                        InvokeValueChangedEvents();
                }
            }
        }

        protected virtual void ChangeCachedValue()
        {
            cachedValue = value;
        }

        public T1 GetValue()
        {
            return Value;
        }

        public void OnEnable()
        {
            if (ChangePermission == VariableChangePermission.ALLOW_TEMPORARY)
            {
                value = cachedValue;
            }
        }

        public void OnDisable()
        {
            if (ChangePermission == VariableChangePermission.ALLOW_TEMPORARY)
                value = cachedValue;
        }

        protected virtual void InvokeValueChangedEvents()
        {
            OnValueChanged.Invoke();
        }
    }
}
