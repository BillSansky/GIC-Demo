using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace BFT
{

    public class AdvancedInvertablePlayableDirector : AdvancedPlayableDirector
    {
        private bool doUpdate;

        [FoldoutGroup("Events")] public UnityEvent OnPlayingBackwardsDone;
        [FoldoutGroup("Events")] public UnityEvent OnPlayingForwardsDone;
        [BoxGroup("Update Options")] public bool PlayInverted;

        public override bool IsDone
        {
            get => base.IsDone;
            protected set
            {
                base.IsDone = value;
                if (IsDone)
                {
                    if (PlayInverted)
                        OnPlayingBackwardsDone.Invoke();
                    else
                    {
                        OnPlayingForwardsDone.Invoke();
                    }
                }
            }
        }

        public void Awake()
        {
            AutoCheckAndPlay = false;
        }

        public override void Play()
        {
            base.Play();
            StartLogic();
        }

        public void SetInverted(bool inv)
        {
            PlayInverted = inv;
        }

        private void StartLogic()
        {
            Director.timeUpdateMode = DirectorUpdateMode.Manual;
            Director.Play();
            StopAllCoroutines();
            StartCoroutine(UpdateLogic());
        }

        public void RestartForward()
        {
            Director.Stop();
            Director.time = 0;
            Director.Evaluate();
            PlayInverted = false;
            IsDone = false;
            StartLogic();
        }

        public void RestartBackward()
        {
            Director.Stop();
            Director.time = Director.duration;
            Director.Evaluate();
            PlayInverted = true;
            IsDone = false;
            StartLogic();
        }

        public void PlayForward()
        {
            PlayInverted = false;
            StartLogic();
        }

        public void PlayBackward()
        {
            PlayInverted = true;
            StartLogic();
        }

        public void SwitchInverted()
        {
            PlayInverted = !PlayInverted;
        }

        public override void Stop()
        {
            base.Stop();
            if (PlayInverted)
            {
                Director.time = Director.duration;
                Director.Evaluate();
            }
            else
            {
                Director.time = 0;
                Director.Evaluate();
            }
        }

        IEnumerator UpdateLogic()
        {
            while (!IsDone || Director.extrapolationMode != DirectorWrapMode.None)
            {
                float delta = UseSpeedMultiplier ? UnityEngine.Time.deltaTime * PlaySpeedMultiplier.Value : UnityEngine.Time.deltaTime;
                if (!PlayInverted)
                {
                    Director.time += delta;

                    if (Director.time >= Director.duration)
                    {
                        Director.time = Director.duration;
                        if (Director.extrapolationMode == DirectorWrapMode.Loop)
                            Director.time = 0;

                        if (!IsDone)
                            IsDone = true;
                    }

                    Director.Evaluate();
                }
                else
                {
                    Director.time -= delta;

                    if (Director.time <= 0)
                    {
                        Director.time = 0;

                        if (Director.extrapolationMode == DirectorWrapMode.Loop)
                            Director.time = Director.duration;

                        if (!IsDone)
                            IsDone = true;
                    }

                    Director.Evaluate();
                }

                yield return null;
            }
        }
    }
}
