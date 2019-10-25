using System;
using BFT;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Events;

namespace Plugins.BFT.Physics.Triggers.Detection
{
    /// <summary>
    ///     A detection trigger is able to store a reference to the object that was detected for excternal usages
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractDetectionTrigger<T> : MonoBehaviour, IValue<T>
    {
        [Serializable]
        public delegate void TriggerAction(AbstractDetectionTrigger<T> detector);

        [FoldoutGroup("Utils")] public bool LogTrigger;

        [BoxGroup("Events"), PreviouslySerializedAs("OnTriggerEnter")]
        public UnityEvent OnTriggerEnterEvent;

        [BoxGroup("Events"), PreviouslySerializedAs("OnTriggerExit")]
        public UnityEvent OnTriggerExitEvent;

        private T value;

        [InfoBox("A detection trigger is able to store a reference to the object that was detected for external usages")]
        public abstract T LastDetectedObject { get; protected set; }

        public abstract bool IsTriggered { get; protected set; }

        public T Value => LastDetectedObject;

        public event TriggerAction OnTriggerEntered;
        public event TriggerAction OnTriggerExited;

        protected void InvokeTriggerEntered()
        {
            if (LogTrigger)
                UnityEngine.Debug.Log("Trigger Entered", this);


            if (OnTriggerEntered != null) OnTriggerEntered(this);
            OnTriggerEnterEvent.Invoke();
        }

        protected void InvokeTriggerExited()
        {
            if (LogTrigger)
                UnityEngine.Debug.Log("Trigger Exited", this);

            if (OnTriggerExited != null) OnTriggerExited(this);
            OnTriggerExitEvent.Invoke();
        }
    }
}