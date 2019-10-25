using System.Collections;
using UnityEngine;

namespace BFT
{
    public class TimeScaler : MonoBehaviour
    {
        private float previousTimeScale;
        public bool RestoreScaleOnDisable;

        public bool ScaleOnEnable;
        public float TimeScale;

        private void OnEnable()
        {
            if (ScaleOnEnable)
            {
                ScaleTime();
            }
        }

        private void OnDisable()
        {
            if (RestoreScaleOnDisable)
                RestoreScale();
        }

        public void ScaleTime()
        {
            previousTimeScale = UnityEngine.Time.timeScale;
            UnityEngine.Time.timeScale = TimeScale;
        }

        public void RestoreScale()
        {
            UnityEngine.Time.timeScale = previousTimeScale;
        }

        public void ScaleTimeForSeconds(float realtimeSeconds)
        {
            StartCoroutine(ScaleTimeForAWhile(realtimeSeconds));
        }

        private IEnumerator ScaleTimeForAWhile(float realtimeSeconds)
        {
            ScaleTime();
            yield return new WaitForSecondsRealtime(realtimeSeconds);
            RestoreScale();
        }
    }
}
