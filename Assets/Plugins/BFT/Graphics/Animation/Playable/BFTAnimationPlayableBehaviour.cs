using System;
using UnityEngine;
using UnityEngine.Playables;

namespace BFT
{
    public class BFTAnimationPlayableBehaviour : PlayableBehaviour
    {
        public const bool DebugTrain = false;
        public bool HoldOnDone;
        private bool isGoingForwards;

        private double lastTime;

        public bool PlayReversed;
        private AnimationUpdateType previousType;
        public IAnimation Target;

        private float Time(UnityEngine.Playables.Playable playable)
        {
            if (PlayReversed)
            {
                return (float) (playable.GetDuration() - playable.GetTime());
            }

            return (float) playable.GetTime();
        }

        public void UpdateTime(float newTime)
        {
            isGoingForwards = newTime > lastTime;
            lastTime = newTime;
        }

        public override void OnBehaviourPlay(UnityEngine.Playables.Playable playable, FrameData info)
        {
            //TODO add a function in all animation to "reset init state" for when it stops,
            //TODO because it's not enough in case of from tween

            Target.UpdateMode = AnimationUpdateType.MANUAL;
            Target.Play();
            Target?.GoToTime(Time(playable));
        }

        public override void OnGraphStart(UnityEngine.Playables.Playable playable)
        {
            previousType = Target.UpdateMode;
            Target.UpdateMode = AnimationUpdateType.MANUAL;
            //directly jump to the start time
            Target?.GoToTime(Time(playable));
            playable.SetPropagateSetTime(true);
        }

        public override void PrepareFrame(UnityEngine.Playables.Playable playable, FrameData info)
        {
        }

        public override void PrepareData(UnityEngine.Playables.Playable playable, FrameData info)
        {
        }

        public override void OnGraphStop(UnityEngine.Playables.Playable playable)
        {
            //reset when stoppping the graph
            Target.GoToTime(0);
            Target.UpdateMode = previousType;
        }

        [Obsolete("OnBehaviourDelay is obsolete; use a custom ScriptPlayable to implement this feature", false)]
        public override void OnBehaviourDelay(UnityEngine.Playables.Playable playable, FrameData info)
        {
        }

        public override void OnBehaviourPause(UnityEngine.Playables.Playable playable, FrameData info)
        {
            if (Target == null)
            {
                return;
            }

            UpdateTime(Time(playable));

            if (Target.IsPlaying)
            {
                //do nothing, as we should hold the current time, and the animation is already in manual mode
                if (isGoingForwards)
                {
                    Target.GoToTime(Target.Duration);
                }
                else
                {
                    Target.GoToTime(0);
                }
            }
            else
            {
                if (!HoldOnDone)
                {
                    Target.GoToTime(0);
                    //reset to the inital time
                }

                //do nothing to hold the last value in place          
            }
        }

        public override void ProcessFrame(UnityEngine.Playables.Playable playable, FrameData info, object playerData)
        {
            Target?.GoToTime(Time(playable));
            UpdateTime((float) playable.GetTime());

#if UNITY_EDITOR
            if (!Application.isPlaying)
                Target.ManualUpdate(0);
#endif
        }
    }
}
