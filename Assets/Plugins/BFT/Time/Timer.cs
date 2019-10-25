using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

#if UNITY_EDITOR
#endif

namespace BFT
{
    [ExecuteInEditMode]
    public class Timer : MonoBehaviour, IValue<float>, IValue<bool>
    {
        [TabGroup("Status", order: 1), ShowInInspector, ReadOnly]
        private float currentTime;

        [BoxGroup("Timer")] [SerializeField] private FloatValue duration;

        [BoxGroup("Initialization")] public bool IgnoreTimeScale;

        [TabGroup("Utils")] public bool LogTimer;

        [BoxGroup("Events"), FormerlySerializedAs("onTimerDoneEvent")]
        public UnityEvent OnTimerDoneEvent;

        [BoxGroup("Events"), FormerlySerializedAs("onTimerStartEvent")]
        public UnityEvent OnTimerStartEvent;

        private bool paused = false;

        private bool shouldCount = false;

        [BoxGroup("Initialization")] public bool StartTimerOnEnable;

        [BoxGroup("Initialization")] public bool StartTimerOnStart;

        [BoxGroup("Initialization")] public bool StartTimerPaused;

        public bool ShouldCount => shouldCount;

        [TabGroup("Status"), ShowInInspector, ReadOnly]
        public float Percent
        {
            get
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (Duration == 0)
                    return 1;

                return Mathf.Clamp01(CurrentTime / Duration);
            }
        }

        [TabGroup("Status"), ShowInInspector, ReadOnly]
        public bool IsDone => CurrentTime >= Duration;

        public float Duration => duration.Value;

        public float CurrentTime => currentTime;

        public float TimeLeft => duration.Value - currentTime;

        public bool Paused
        {
            get => paused;
            set => paused = value;
        }

        bool IValue<bool>.Value => IsDone;

        float IValue<float>.Value => Percent;

        public void SetDuration(float value)
        {
            duration.LocalValue = value;
        }

        void Start()
        {
            if (StartTimerPaused)
                paused = true;

            if (StartTimerOnStart)
                StartTimer();
        }

        public void OnEnable()
        {
            if (StartTimerOnEnable)
                StartTimer();
        }

        public void OnDisable()
        {
            StopTimer();
        }

        [TabGroup("Utils"), Button(ButtonSizes.Medium)]
        public void StartTimer()
        {
            StartTimer(0);
        }

        public void StartTimer(float startTime)
        {
            if (LogTimer)
                UnityEngine.Debug.LogFormat(this, "Timer {0} starting", name);
            currentTime = startTime;
            if (!shouldCount)
                StartCoroutine(UpdateTimer());


            OnTimerStartEvent.Invoke();
        }

        public void PauseTimer()
        {
            paused = true;
        }

        public void ResumeTimer()
        {
            paused = false;

            if (LogTimer)
                UnityEngine.Debug.LogFormat(this, "Timer {0} paused", name);
        }


        [TabGroup("Utils"), Button(ButtonSizes.Medium)]
        public void StopTimer()
        {
            shouldCount = false;
            if (IsDone)
                currentTime = 0;

            if (LogTimer)
                UnityEngine.Debug.LogFormat(this, "Timer {0} stopped", name);
        }

        public void StopAndZeroTimer()
        {
            StopTimer();
            currentTime = 0;
        }

        IEnumerator UpdateTimer()
        {
            shouldCount = true;
            while (shouldCount)
            {
                while (Paused && IgnoreTimeScale)
                {
                    yield return null;
                }

                //#if UNITY_EDITOR
                //            bool unPaused = false;
                //            while (EditorApplication.isPaused)
                //            {
                //                unPaused = true;
                //                yield return null;
                //            }

                //            if (unPaused)
                //            {
                //                //make sure the pause time will not be taken into account
                //                yield return new WaitForEndOfFrame();
                //            }
                //#endif
                currentTime = CurrentTime +
                              ((IgnoreTimeScale) ? UnityEngine.Time.unscaledDeltaTime : UnityEngine.Time.deltaTime);

                if (CurrentTime >= Duration)
                {
                    currentTime = Duration;
                    shouldCount = false;

                    OnTimerDoneEvent.Invoke();
                    yield break;
                }

                yield return null;
            }
        }
    }
}