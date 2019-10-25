using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace BFT
{
    public class OnAwakeEvent : MonoBehaviour
    {
        [FormerlySerializedAs("OnEnabled")] [SerializeField]
        private UnityEvent OnAwake;

        private void Awake()
        {
            OnAwake.Invoke();
        }
    }
}
