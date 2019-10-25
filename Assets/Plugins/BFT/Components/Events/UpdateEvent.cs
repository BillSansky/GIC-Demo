using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    public class UpdateEvent : MonoBehaviour
    {
        private float elapsed;
        [SerializeField] private UnityEvent OnUpdated;
        [SerializeField] private float tickFrequency = 0;

        void Update()
        {
            elapsed += UnityEngine.Time.deltaTime;
            if (elapsed > tickFrequency)
            {
                elapsed = 0;
                OnUpdated.Invoke();
            }
        }
    }
}
