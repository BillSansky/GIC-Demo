using UnityEngine;

namespace BFT
{
    public class AdvancedPlayableLooperDirector : AdvancedPlayableDirector
    {
        public bool ReversedLoop;

        public void Awake()
        {
            AutoCheckAndPlay = false;
        }

        public void Update()
        {
            if (IsDone)
                return;
            if (!ReversedLoop && Director.time > Director.duration)
                Director.time = 0;
            else if (ReversedLoop && Director.time <= 0)
                Director.time = Director.duration;
            Director.Evaluate();
        }
    }
}
