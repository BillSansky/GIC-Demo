using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace BFT
{
    public enum EDirectorButtonState
    {
        NEUTRAL,
        PRESSING,
        RELEASING,
        CANCELING,
    }
    
    public class AdvancedPlayableButtonAnimationDirector : AdvancedPlayableDirector
    {
        public UnityEvent OnPressingDone;
        public UnityEvent OnReleasingDone;

        private bool pressingDone;
        public bool PressSyncWithRelease;

        public float ReleaseTime;

        public bool ReleaseWaitsForPressFinish;

        [ShowInInspector, ReadOnly] private EDirectorButtonState state;


        public void Awake()
        {
            AutoCheckAndPlay = false;
        }

        public override void Start()
        {
            base.Start();
            Director.timeUpdateMode = DirectorUpdateMode.Manual;
        }

        public void PressButton()
        {
            if (PressSyncWithRelease && state == EDirectorButtonState.RELEASING)
                Director.time = Mathf.Max(0, (float) Director.time - ReleaseTime);
            else
            {
                Director.time = 0;
            }

            state = EDirectorButtonState.PRESSING;

            Play();
            StopAllCoroutines();
            StartCoroutine(ButtonLogic());
            pressingDone = false;
        }

        public void ReleaseButton()
        {
            if (!ReleaseWaitsForPressFinish)
                Director.time = Mathf.Min(ReleaseTime, (float) Director.time);

            state = EDirectorButtonState.RELEASING;

            Play();
            StopAllCoroutines();
            StartCoroutine(ButtonLogic());
        }

        public void CancelButtonPressed()
        {
            if (state == EDirectorButtonState.RELEASING)
                return;

            state = EDirectorButtonState.CANCELING;

            if (IsDone)
            {
                IsDone = false;
                Play();
                StartCoroutine(ButtonLogic());
            }
        }

        private IEnumerator ButtonLogic()
        {
            while (!IsDone)
            {
                switch (state)
                {
                    case EDirectorButtonState.PRESSING:
                        if (Director.time < ReleaseTime)
                            Director.time += UnityEngine.Time.deltaTime;
                        else if (!pressingDone)
                        {
                            pressingDone = true;
                            OnPressingDone.Invoke();
                        }

                        break;
                    case EDirectorButtonState.RELEASING:
                        if (Director.time < Director.duration)
                            Director.time += UnityEngine.Time.deltaTime;
                        else
                        {
                            state = EDirectorButtonState.NEUTRAL;
                            OnReleasingDone.Invoke();
                        }

                        break;
                    case EDirectorButtonState.CANCELING:
                        if (Director.time > 0)
                            Director.time -= UnityEngine.Time.deltaTime;
                        else
                        {
                            state = EDirectorButtonState.NEUTRAL;
                            IsDone = true;
                        }

                        break;
                    case EDirectorButtonState.NEUTRAL:
                        IsDone = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                Director.Evaluate();
                yield return null;
            }
        }
    }
}
