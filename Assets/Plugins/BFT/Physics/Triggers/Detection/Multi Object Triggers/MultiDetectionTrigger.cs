using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Plugins.BFT.Physics.Triggers.Detection.Multi_Object_Triggers
{
    /// <summary>
    ///     This trigger checks all the triggers attached to it, and whenever they detect something, holds a reference to it.
    ///     It will consider itself triggered as long as it has objects that were detected
    ///     by one or more triggers that did not exit all those triggers yet.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MultiDetectionTrigger<T> : AbstractDetectionTrigger<List<T>> where T : UnityEngine.Object
    {
        [ShowInInspector, FoldoutGroup("Status")]
        protected readonly List<T> CurrentObjectDetected = new List<T>(3);

        [Tooltip(
            " This trigger checks all the triggers attached to it, and whenever they detect something, holds a reference to it." +
            "It will consider itself triggered as long as it has objects that were detected +by one or more triggers that did not exit all those triggers yet.")]
        private readonly Dictionary<T, List<AbstractDetectionTrigger<T>>> triggersThatDetected =
            new Dictionary<T, List<AbstractDetectionTrigger<T>>>();

        [FoldoutGroup("Events")] public bool InvokeObjectExitedOnDestroy = true;

        [FoldoutGroup("Events")] public UnityEvent OnFirstObjectEntered;

        [FoldoutGroup("Events")] public UnityEvent OnLastObjectExited;

        [FoldoutGroup("Events")] public UnityEvent OnNewObjectEntered;

        [FoldoutGroup("Events")] public UnityEvent OnObjectExited;

        [BoxGroup("Triggers")] public List<AbstractDetectionTrigger<T>> Triggers;

        [ShowInInspector, FoldoutGroup("Status")]
        public override bool IsTriggered { get; protected set; }

        public void AddTrigger(AbstractDetectionTrigger<T> detectionTrigger)
        {
            if (!Triggers.Contains(detectionTrigger))
            {
                Triggers.Add(detectionTrigger);
                detectionTrigger.OnTriggerExited += NotifyTriggerOut;
                detectionTrigger.OnTriggerEntered += NotifyTriggerOn;
            }
        }

        public void RemoveTrigger(AbstractDetectionTrigger<T> detectionTrigger)
        {
            if (Triggers.Contains(detectionTrigger))
            {
                Triggers.Remove(detectionTrigger);
                detectionTrigger.OnTriggerExited -= NotifyTriggerOut;
                detectionTrigger.OnTriggerEntered -= NotifyTriggerOn;
            }
        }

        void Awake()
        {
            foreach (var trigger in Triggers)
            {
                trigger.OnTriggerExited += NotifyTriggerOut;
                trigger.OnTriggerEntered += NotifyTriggerOn;
            }
        }

        void OnDestroy()
        {
            foreach (var detector in Triggers)
            {
                detector.OnTriggerExited -= NotifyTriggerOut;
                detector.OnTriggerEntered -= NotifyTriggerOn;
            }
        }

        void NotifyTriggerOn(AbstractDetectionTrigger<T> detectionTrigger)
        {
            if (!triggersThatDetected.ContainsKey(detectionTrigger.LastDetectedObject))
            {
                triggersThatDetected.Add(detectionTrigger.LastDetectedObject, new List<AbstractDetectionTrigger<T>>());
                triggersThatDetected[detectionTrigger.LastDetectedObject].Add(detectionTrigger);

                if (!CurrentObjectDetected.Contains(detectionTrigger.LastDetectedObject))
                    CurrentObjectDetected.Add(detectionTrigger.LastDetectedObject);
                if (!IsTriggered)
                {
                    IsTriggered = true;
                    InvokeTriggerEntered();
                    OnFirstObjectEntered.Invoke();
                }
                else
                {
                    OnNewObjectEntered.Invoke();
                }
            }
            else
            {
                //the object was already detected, just note down that the trigger detected it.
                triggersThatDetected[detectionTrigger.LastDetectedObject].Add(detectionTrigger);
            }
        }

        void NotifyTriggerOut(AbstractDetectionTrigger<T> detectionTrigger)
        {
            if (!triggersThatDetected.ContainsKey(detectionTrigger.LastDetectedObject))
                return;

            triggersThatDetected[detectionTrigger.LastDetectedObject].Remove(detectionTrigger);

            if (triggersThatDetected[detectionTrigger.LastDetectedObject].Count != 0)
                return;

            triggersThatDetected.Remove(detectionTrigger.LastDetectedObject);

            if (CurrentObjectDetected.Contains(detectionTrigger.LastDetectedObject))
            {
                CurrentObjectDetected.Remove(detectionTrigger.LastDetectedObject);

                if (CurrentObjectDetected.Count == 0)
                {
                    IsTriggered = false;
                    OnLastObjectExited.Invoke();
                    InvokeTriggerExited();
                }
                else
                {
                    OnObjectExited.Invoke();
                }
            }
        }

        public void Update()
        {
            if (CurrentObjectDetected.All(_ => _ == null))
            {
                CurrentObjectDetected.Clear();
                IsTriggered = false;
                if (InvokeObjectExitedOnDestroy)
                    OnLastObjectExited.Invoke();
            }

            for (int i = CurrentObjectDetected.Count - 1; i >= 0; i--)
            {
                if (!CurrentObjectDetected[i] || CurrentObjectDetected[i] == null)
                {
                    CurrentObjectDetected.RemoveAt(i);
                    if (InvokeObjectExitedOnDestroy)
                        OnObjectExited.Invoke();
                }
            }
        }
    }
}