using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    [AddComponentMenu("Referencing/Percent/Bool To Percent")]
    public class BoolToPercentValue : SerializedMonoBehaviour, IValue<float>
    {
        public bool AuthorizeChangeWhenTransitioning = false;

        private bool goingUp;

        public bool IncrementOnTrueOnly = true;

        public bool InitialValue;
        private bool isTrue;

        private float percent;
        public float ToEmptySpeed = 1;

        public float ToFullSpeed = 1;

        private bool transitioning;

        public bool IsTrue
        {
            get => isTrue;
            set
            {
                isTrue = value;

                if (AuthorizeChangeWhenTransitioning || !transitioning)
                {
                    if (IncrementOnTrueOnly && isTrue || !IncrementOnTrueOnly)
                    {
                        goingUp = !goingUp;
                        StopAllCoroutines();
                        StartCoroutine(IncrementPercent());
                    }
                }
            }
        }

        public float Value => percent;

        public void Start()
        {
            goingUp = InitialValue;
            if (!goingUp)
                percent = 0;
            else
            {
                percent = 1;
            }
        }

        public IEnumerator IncrementPercent()
        {
            transitioning = true;
            while (true)
            {
                percent += (goingUp) ? ToFullSpeed * UnityEngine.Time.deltaTime : -ToEmptySpeed * UnityEngine.Time.deltaTime;

                if (percent >= 1 || percent <= 0)
                {
                    percent = Mathf.Clamp01(percent);
                    transitioning = false;
                    break;
                }

                yield return null;
            }
        }
    }
}
