using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;

namespace BFT
{
    public class PlayableDirectorPercent : MonoBehaviour
    {
        [SerializeField] private PlayableDirector director;

        public PercentValue PercentValue;


        [FormerlySerializedAs("StartOnAwake")] public bool PlayOnEnable = true;

        void Reset()
        {
            director = GetComponent<PlayableDirector>();
        }

        void OnEnable()
        {
            if (PlayOnEnable)
                Play();
        }

        public void Play()
        {
            director.timeUpdateMode = DirectorUpdateMode.Manual;
            director.initialTime = PercentValue.Value * director.duration;

            StartCoroutine(UpdateDirector());
        }


        private IEnumerator UpdateDirector()
        {
            while (true)
            {
                EvaluateDirector();
                yield return null;
            }
        }

        private void EvaluateDirector()
        {
            director.time = PercentValue.Value * director.duration;
            director.Evaluate();
        }


#if UNITY_EDITOR
        public void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying)
                EvaluateDirector();
        }
#endif
    }
}
