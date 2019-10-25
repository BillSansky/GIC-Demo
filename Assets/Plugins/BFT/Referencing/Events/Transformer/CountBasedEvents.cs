using System;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace BFT
{
    public class CountBasedEvents : SerializedMonoBehaviour
    {
        public bool CheckCountOnEnable = false;
        public Func<int> Counter;
        public UnityEvent OnMoreThanOneLeft;

        public UnityEvent OnNoMoreLeft;
        public UnityEvent OnOneOnlyLeft;


        public void OnEnable()
        {
            if (CheckCountOnEnable)
                CheckCount();
        }

        public void CheckCount()
        {
            int count = Counter();
            if (count == 0)
            {
                OnNoMoreLeft.Invoke();
            }
            else if (count == 1)
            {
                OnOneOnlyLeft.Invoke();
            }
            else
            {
                OnMoreThanOneLeft.Invoke();
            }
        }
    }
}
