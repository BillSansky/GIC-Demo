#if BFT_DOTWEEN
using System;
using DG.Tweening;
using DG.Tweening.Core;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace BFT
{
    public enum TweenTarget
    {
        FROM,
        TO
    }

    public abstract class GeneralTweener<T, T1, T2, T3> : MonoBehaviour, ITweenValue
        where T2 : Action<T> where T1 : Function<T> where T3 : IValue<T>
    {
        [FoldoutGroup("Option", order: 999)] public bool CreateTweenOnAwake = true;

        [BoxGroup("Easing")] public float EaseOverShootValue;

        [ShowIf("IsEasingWithCurve")] [BoxGroup("Easing")]
        public AnimationCurve EasingCurve;

        [BoxGroup("Easing")] public Ease EasingMode = Ease.Unset;

        [FormerlySerializedAs("TweenValue")] [BoxGroup("Start And End")]
        public T3 EndValue;

        [BoxGroup("Loop"), PreviouslySerializedAs("LoopAmount"), HideIf("InfiniteLoop")]
        public int ExtraLoopAmount;

        [FoldoutGroup("Option")] public bool ForceRebuildOfTween = true;
        [BoxGroup("Target"), ShowIf("IsTo")] public bool IsRelativeToFrom;

        [SerializeField] [BoxGroup("Events")] private UnityEvent onEnd;

        [SerializeField] [BoxGroup("Events")] private UnityEvent onPause;


        [SerializeField] [BoxGroup("Events")] private UnityEvent onPlay;
        [BoxGroup("Init")] public bool PlayOnEnable = true;

        [BoxGroup("Durations")] public bool SpeedBasedMode;


        [BoxGroup("Durations")] public float StartDelay;


        [FormerlySerializedAs("InitialValue")] [BoxGroup("Start And End")] [ShowIf("StaticInitialValue")]
        public T StartValue;

        [BoxGroup("Start And End")] public bool StaticInitialValue = true;
        protected Tweener Tween;

        [LabelText("$DurationName")] [BoxGroup("Durations")]
        public FloatValue TweenDuration;

        [BoxGroup("Target")] public TweenTarget TweenTarget = TweenTarget.TO;

        [BoxGroup("Loop")] public UpdateType UpdateType = UpdateType.Normal;

        [BoxGroup("Target")] public T1 ValueGetter;

        [BoxGroup("Target")] public T2 ValueSetter;

        public DOGetter<T> DoGetter => ValueGetter.GetValue;

        public DOSetter<T> DoSetter => ValueSetter.Invoke;
        public bool IsEasingWithCurve => EasingMode == Ease.Unset;
        private bool IsTo => TweenTarget == TweenTarget.TO;

        [BoxGroup("Loop")]
        [ShowInInspector]
        public bool InfiniteLoop
        {
            get => ExtraLoopAmount < 0;
            set => ExtraLoopAmount = value ? -1 : 0;
        }

        private string DurationName => SpeedBasedMode ? "Speed" : "Duration";

        public AnimationUpdateType UpdateMode
        {
            get => UpdateType == UpdateType.Manual ? AnimationUpdateType.MANUAL : AnimationUpdateType.AUTO;
            set
            {
                if (value == AnimationUpdateType.MANUAL)
                {
                    UpdateType = UpdateType.Manual;
                    if (Tween != null)
                    {
                        Tween.SetUpdate(UpdateType.Manual);
                    }
                }
                else
                {
                    if (UpdateType == UpdateType.Manual)
                    {
                        UpdateType = UpdateType.Normal;
                        if (Tween != null)
                        {
                            Tween.SetUpdate(UpdateType.Normal);
                        }
                    }
                }
            }
        }


        public bool IsPlaying => Tween != null && Tween.IsPlaying();
        public float Duration => TweenDuration.Value;

        public UnityEvent OnPlay => onPlay;
        public UnityEvent OnEnd => onEnd;
        public UnityEvent OnPause => onPause;

        public Tween Value
        {
            get
            {
#if UNITY_EDITOR
                RebuildTween();
                Tween.SetUpdate(UpdateType.Manual);

                Tween.ForceInit();
                Tween.SetAutoKill(false);
#endif
                return Tween;
            }
        }

        [BoxGroup("Tools"), Button(ButtonSizes.Medium), HideIf("IsEditMode")]
        public void Play()
        {
            if (Tween == null)
            {
                RebuildTween();
            }

            if (ForceRebuildOfTween)
                RebuildTween();

            if (StaticInitialValue)
            {
                ValueSetter.Invoke(StartValue);
            }

            Tween.Play().OnPlay(OnPlay.Invoke).OnComplete(OnEnd.Invoke);
        }

        [BoxGroup("Tools"), Button(ButtonSizes.Medium), HideIf("IsEditMode")]
        public void Stop()
        {
            Tween.Complete();
            Tween.Rewind();
        }

        [BoxGroup("Tools"), Button(ButtonSizes.Medium), HideIf("IsEditMode")]
        public void Pause()
        {
            Tween.Pause();
            OnPause.Invoke();
        }

        [BoxGroup("Tools"), Button(ButtonSizes.Medium), HideIf("IsEditMode")]
        public void Restart()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                RebuildTween();
            }
#endif

            if (ForceRebuildOfTween)
                RebuildTween();

            if (Tween == null)
            {
                RebuildTween();
            }

            if (StaticInitialValue)
            {
                ValueSetter.Invoke(StartValue);
            }

            Tween.OnComplete(OnEnd.Invoke);
            Tween.OnPlay(OnPlay.Invoke);
            Tween.Restart();
        }

        public void GoToTime(float time)
        {
            if (time <= 0 && StaticInitialValue)
            {
                ValueSetter.Invoke(StartValue);
            }

            Tween.Goto(time);
        }

        public void ManualUpdate(float deltaTime)
        {
            Tween.SetUpdate(UpdateType.Manual);
            DOTween.ManualUpdate(deltaTime, deltaTime);
        }

        public void SetTarget(string id, GameObject target)
        {
            Tween.SetTarget(target);
        }

        [BoxGroup("Tools/Init")]
        [Button(ButtonSizes.Medium)]
        private void StoreInitialValue()
        {
            StartValue = ValueGetter.Value;
        }

        [BoxGroup("Tools/Init")]
        [Button(ButtonSizes.Medium)]
        private void SetInitialValue()
        {
            ValueSetter.Invoke(StartValue);
        }

        public void OnValidate()
        {
            DOTween.Clear();
            DOTween.ClearCachedTweens();
            RebuildTween();
        }

        public virtual void Awake()
        {
            if (CreateTweenOnAwake)
                RebuildTween();
        }


        public void OnEnable()
        {
            if (PlayOnEnable)
                Restart();
        }

        protected virtual void SetGettersAndSetters()
        {
        }

        [BoxGroup("Tools"), Button(ButtonSizes.Medium)]
        public void RebuildTween()
        {
            Tween?.Kill();
            SetGettersAndSetters();
            BuildTween();
            if (EasingMode == Ease.Unset)
            {
                Tween.SetEase(EasingCurve);
            }
            else
            {
                Tween.SetEase(EasingMode);
            }

            Tween.SetSpeedBased(SpeedBasedMode)
                .SetLoops(ExtraLoopAmount)
                .SetRecyclable(true)
                .SetDelay(StartDelay)
                .SetUpdate(UpdateType)
                .SetAutoKill(false);

            if (TweenTarget == TweenTarget.FROM)
            {
                Tween.From();
            }

            Tween.SetRelative(IsRelativeToFrom);
        }

        protected abstract Tween BuildTween();

        public void RebuildAndRestart()
        {
            RebuildTween();
            Restart();
        }

        [BoxGroup("Tools"), Button(ButtonSizes.Medium), HideIf("IsEditMode")]
        public void RestartBackWards()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                RebuildTween();
            }
#endif
            if (Tween == null)
            {
                RebuildTween();
            }

            Tween.Goto(Duration);
            Tween.PlayBackwards();
        }

#if UNITY_EDITOR

        private bool IsEditMode => !Application.isPlaying;

        [Button(ButtonSizes.Medium), BoxGroup("Tools"), ShowIf("IsEditMode")]
        public void PlayPreview()
        {
            RebuildTween();

            Tween.SetUpdate(UpdateType.Manual);

            Tween.ForceInit();
            Tween.SetAutoKill(false);

            if (StaticInitialValue)
            {
                ValueSetter.Invoke(StartValue);
            }

            Tween.Restart(false);

            EditorUpdate.EditorUpdatePlug(EditorPlay);
        }

        [Button(ButtonSizes.Medium), BoxGroup("Tools"), ShowIf("IsEditMode")]
        public void StopPreview()
        {
            try
            {
                Tween?.Complete();

                if (TweenTarget == TweenTarget.TO)
                {
                    Tween.Rewind();
                }
            }
            catch (Exception)
            {
                // ignored
            }

            EditorUpdate.RemoveEditorUpdatePlug(EditorPlay);
        }

        public void EditorPlay(double deltaTime)
        {
            DOTween.ManualUpdate((float) deltaTime, (float) deltaTime);

            if (Tween != null && Tween.IsComplete())
            {
                if (TweenTarget == TweenTarget.TO)
                {
                    Tween.Rewind(false);
                }

                EditorUpdate.RemoveEditorUpdatePlug(EditorPlay);
                //reset to normal to avoid trigger by other tweens
                Tween.SetUpdate(UpdateType.Normal);
                DOTween.CompleteAll();
                DOTween.Clear();
                DOTween.ClearCachedTweens();

                if (StaticInitialValue)
                {
                    ValueSetter.Invoke(StartValue);
                }
            }
        }
#endif
    }


}
#endif
