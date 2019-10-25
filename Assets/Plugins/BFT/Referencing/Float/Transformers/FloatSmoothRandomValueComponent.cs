using System.Collections;
using UnityEngine;

namespace BFT
{
    public class FloatSmoothRandomValueComponent : MonoBehaviour, IValue<float>
    {
        private float currentTarget;
        private float currentValue;
        public float From;

        public float reachThresholdValue = 0.01f;
        public float To;

        public float transitionSpeed;


        public float Value => currentValue;

        private void Awake()
        {
            SetCurrentTarget();
            currentValue = currentTarget;
        }

        private void OnEnable()
        {
            StartCoroutine(ValueTransition());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private void SetCurrentTarget()
        {
            currentTarget = Random.Range(From, To);
        }

        private IEnumerator ValueTransition()
        {
            bool reached = false;
            while (true)
            {
                if (currentTarget > currentValue)
                {
                    currentValue += UnityEngine.Time.deltaTime * transitionSpeed;

                    reached = (currentValue >= currentTarget);
                }
                else
                {
                    currentValue -= UnityEngine.Time.deltaTime * transitionSpeed;

                    reached = (currentValue <= currentTarget);
                }


                if (reached)
                {
                    currentValue = currentTarget;
                    SetCurrentTarget();
                }

                yield return null;
            }
        }
    }
}
