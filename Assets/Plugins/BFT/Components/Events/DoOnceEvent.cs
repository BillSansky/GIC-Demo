using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    public class DoOnceEvent : SerializedMonoBehaviour
    {
        [ShowInInspector, ReadOnly] private bool isReady = true;
        [SerializeField] private UnityEvent OnDo;


        [Button(ButtonSizes.Medium)]
        public void Invoke()
        {
            if (isReady)
            {
                isReady = false;
                OnDo.Invoke();
            }
        }

        [Button(ButtonSizes.Medium)]
        public void Restart()
        {
            isReady = true;
        }
    }
}
