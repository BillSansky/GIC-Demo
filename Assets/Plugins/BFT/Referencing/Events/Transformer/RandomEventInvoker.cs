using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    public class RandomEventInvoker : MonoBehaviour
    {
        public float MaxTestFrequency = 1;
        public float MaxTestLuck = 0.5f;
        public float MinTestFrequency = 1;

        [Range(0, 1)] public float MinTestLuck = 0.5f;

        public UnityEvent OnTestSuccess;

        private void OnEnable()
        {
            StartCoroutine(CheckRandom());
        }

        private IEnumerator CheckRandom()
        {
            while (true)
            {
                float luck = Random.Range(MinTestLuck, MaxTestLuck);

                if (Random.value <= luck)
                {
                    OnTestSuccess.Invoke();
                }

                yield return new WaitForSeconds(Random.Range(MinTestFrequency, MaxTestFrequency));
            }
        }
    }
}
