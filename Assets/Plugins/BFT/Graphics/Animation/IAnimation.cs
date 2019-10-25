using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    public enum AnimationUpdateType
    {
        MANUAL,
        AUTO
    }

    /// <summary>
    ///     represents any object that has a duration and
    ///     can be played, stopped, and moved to a specific time.
    /// </summary>
    public interface IAnimation
    {
        AnimationUpdateType UpdateMode { get; set; }

        /// <summary>
        ///     true if the animation was started and is not paused or finished
        /// </summary>
        bool IsPlaying { get; }

        /// <summary>
        ///     how long the animation is.
        /// </summary>
        float Duration { get; }

        UnityEvent OnPlay { get; }
        UnityEvent OnEnd { get; }
        UnityEvent OnPause { get; }

        /// <summary>
        ///     starts the animation from its current position
        /// </summary>
        void Play();

        /// <summary>
        ///     Stops the animation and resets it to its initial position (like on a tape reader)
        /// </summary>
        void Stop();

        /// <summary>
        ///     Pauses the animation,  maintaining it at its current time
        /// </summary>
        void Pause();

        /// <summary>
        ///     Equivalent to Stop, then Play
        /// </summary>
        void Restart();

        /// <summary>
        ///     Instantly forwards or rewinds to a given time
        /// </summary>
        /// <param name="time"></param>
        void GoToTime(float time);

        /// <summary>
        ///     manually update the animation given a specific delta time.
        ///     If the animation is not started, this should do anything.
        /// </summary>
        /// <param name="deltaTime"></param>
        void ManualUpdate(float deltaTime);

        /// <summary>
        ///     Set the target of the animation if it can be applied to different things (quite blurry for now)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="target"></param>
        void SetTarget(string id, GameObject target);
    }
}
