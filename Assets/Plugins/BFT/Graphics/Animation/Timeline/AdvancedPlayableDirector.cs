using System;
using System.Collections;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace BFT
{
    [RequireComponent(typeof(PlayableDirector))]
    public class AdvancedPlayableDirector : SerializedMonoBehaviour,
        IValue<float>, IAnimation
    {
        protected bool AutoCheckAndPlay = true;

        private PlayableDirector director;
        [BoxGroup("Initialization")] public bool EvaluateDirectorOnStart;

        private bool isDone;

        [FoldoutGroup("Events")] public UnityEvent OnBeforePlay;
        [FoldoutGroup("Events")] public UnityEvent OnPauseStart;
        [FoldoutGroup("Events")] public UnityEvent OnPlayEnd;
        [FoldoutGroup("Events")] public UnityEvent OnPlayStart;
        [BoxGroup("Initialization")] public bool PlayOnEnable = false;
        [BoxGroup("Initialization")] public bool PlayOnStart = false;

        [BoxGroup("Update Options"), ShowIf("UseSpeedMultiplier")]
        public IValue<float> PlaySpeedMultiplier = new GenericValue<float>(1);

        private double previousTime;

        [BoxGroup("Update Options")] public bool UnscaledDeltaTime = true;

        [BoxGroup("Update Options")] public bool UseSpeedMultiplier;

        protected PlayableDirector Director
        {
            get { return director.IfNull(() => director = GetComponent<PlayableDirector>()); }
        }

        [BoxGroup("Status"), ShowInInspector, ReadOnly]
        public virtual bool IsDone
        {
            get => isDone;
            protected set
            {
                if (!isDone && value)
                {
                    isDone = true;
                    OnPlayEnd.Invoke();
                }
                else
                {
                    isDone = value;
                }
            }
        }

        [BoxGroup("Status"), ShowInInspector, ReadOnly]
        public float Percent
        {
            get
            {
#if UNITY_EDITOR
                if (!Director || Director.duration <= 0)
                {
                    return 0;
                }
#endif
                return (float) (Director.time / Director.duration);
            }
        }

        public AnimationUpdateType UpdateMode
        {
            get => AutoCheckAndPlay ? AnimationUpdateType.AUTO : AnimationUpdateType.MANUAL;
            set => AutoCheckAndPlay = value == AnimationUpdateType.AUTO;
        }

        public bool IsPlaying => director.state == PlayState.Playing;
        public float Duration => (float) director.duration;

        public UnityEvent OnPlay => OnPlayStart;
        public UnityEvent OnEnd => OnPlayEnd;
        public UnityEvent OnPause => OnPauseStart;

        public void GoToTime(float time)
        {
            Director.Play();

            Director.time = time > Director.duration ? Director.duration : time;
            Director.Evaluate();
            if (time > director.duration)
                isDone = true;
        }

        public void ManualUpdate(float deltaTime)
        {
            director.time += deltaTime;
            Director.Evaluate();
        }

        public void SetTarget(string id, GameObject target)
        {
            PlayableBinding binding = director.playableAsset.outputs
                .First(_ => _.sourceObject.name == id);
            director.SetGenericBinding(binding.sourceObject, target);
        }

        [BoxGroup("Utils"), Button(ButtonSizes.Medium)]
        public virtual void Play()
        {
            IsDone = false;

            OnBeforePlay.Invoke();

            Director.timeUpdateMode = DirectorUpdateMode.Manual;

            if (AutoCheckAndPlay)
            {
                Director.time = 0;
                //StopAllCoroutines();
                StartCoroutine(PlayAndCheck());
            }

            Director.Play();

            OnPlayStart.Invoke();
        }

        [BoxGroup("Utils"), Button(ButtonSizes.Medium)]
        public virtual void Stop()
        {
            Director.Stop();
            StopAllCoroutines();
            IsDone = true;
        }

        public void Pause()
        {
            Director.Pause();
        }

        public void Restart()
        {
            Stop();
            Play();
        }

        public float Value => Percent;

        void Reset()
        {
            director = GetComponent<PlayableDirector>();
            director.playOnAwake = false;
        }

        public virtual void Start()
        {
            if (EvaluateDirectorOnStart)
            {
                Director.Evaluate();
            }

            if (PlayOnStart)
            {
                Play();
            }
        }

        private void OnEnable()
        {
            if (PlayOnEnable)
            {
                Play();
            }
        }


        private IEnumerator PlayAndCheck()
        {
            yield return null;

            while (!IsDone || Director.extrapolationMode != DirectorWrapMode.None)
            {
                while (!Director.isActiveAndEnabled)
                {
                    yield return null;
                }

#if UNITY_EDITOR

                while (UnityEditor.EditorApplication.isPaused && UnscaledDeltaTime)
                {
                    yield return null;
                }

#endif

                float timeAdded = (UnscaledDeltaTime) ? UnityEngine.Time.unscaledDeltaTime : UnityEngine.Time.deltaTime;
                if (UseSpeedMultiplier)
                    timeAdded *= PlaySpeedMultiplier.Value;

                Director.time = Math.Min(Director.duration, Director.time + timeAdded);


                if (Director.time >= Director.duration)
                {
                    IsDone = true;
                    if (Director.extrapolationMode == DirectorWrapMode.Hold)
                        Director.time = Director.duration;
                    else if (Director.extrapolationMode == DirectorWrapMode.Loop)
                    {
                        Director.time = 0;
                    }
                }

                Director.Evaluate();

                yield return null;
            }
        }

        private void OnDrawGizmosSelected()
        {
            /* if (director.time-previousTime>0.0001)
         {
             previousTime = director.time;
             //force evaluation of the whole graph
             director.Evaluate();

         }*/
        }
    }
}
