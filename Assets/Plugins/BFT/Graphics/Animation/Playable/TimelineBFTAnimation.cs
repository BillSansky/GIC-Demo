using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    public class TimelineBFTAnimation : MonoBehaviour, IAnimation
    {
        [SerializeField, OnValueChanged("CheckAnim")]
        private UnityEngine.Object animation;

        private IAnimation animationReference;

        public bool IsPlaying => animationReference.IsPlaying;

        public float Duration => animationReference.Duration;

        public UnityEvent OnPlay => animationReference.OnPlay;

        public UnityEvent OnEnd => animationReference.OnEnd;

        public UnityEvent OnPause => animationReference.OnPause;

        public void Play()
        {
            animationReference.Play();
        }

        public void Stop()
        {
            animationReference.Stop();
        }

        public AnimationUpdateType UpdateMode
        {
            get => animationReference.UpdateMode;
            set => animationReference.UpdateMode = value;
        }

        public void Pause()
        {
            animationReference.Pause();
        }

        public void Restart()
        {
            animationReference.Restart();
        }

        public void GoToTime(float time)
        {
            animationReference.GoToTime(time);
        }

        public void ManualUpdate(float deltaTime)
        {
            animationReference.ManualUpdate(deltaTime);
        }

        public void SetTarget(string id, GameObject target)
        {
            animationReference.SetTarget(id, target);
        }

        public void Reset()
        {
            animation = gameObject;
            CheckAnim();
        }

        private void CheckAnim()
        {
            InterfaceUtils.GetObjectAfterTypeCheck<IAnimation>(ref animation);
        }
    }
}
