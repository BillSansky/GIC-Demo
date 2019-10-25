using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    public class Counter : MonoBehaviour
    {
        public bool CountDown;

        [BoxGroup("Status"), ShowInInspector] private int currentCount;

        public UnityEvent OnCountDone;

        public bool ResetOnCountDown = true;

        [MinValue(0)] public int StartCount;

        [MinValue(0), HideIf("CountDown")] public int TotalCount;

        public int CurrentCount => currentCount;

        void OnEnable()
        {
            currentCount = StartCount;
        }

        private void InvokeCountDone()
        {
            OnCountDone.Invoke();
            if (ResetOnCountDown)
                currentCount = StartCount;
        }

        public void Count()
        {
            if (CountDown)
            {
                currentCount--;
            }
            else
            {
                currentCount++;
            }

            if ((CountDown && currentCount <= 0) || (currentCount >= TotalCount))
                InvokeCountDone();
        }
    }
}
