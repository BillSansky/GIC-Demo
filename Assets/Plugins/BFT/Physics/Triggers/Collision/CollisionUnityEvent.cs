using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Plugins.BFT.Physics.Triggers.Collision
{
    public class CollisionUnityEvent : MonoBehaviour
    {
        public delegate void OnCollisionAction(UnityEngine.Collision collision);

        [BoxGroup("Events")] public UnityEvent CollisionEnter;

        [BoxGroup("Events")] public UnityEvent CollisionExit;

        public event OnCollisionAction OnCollisionEnterEvt, OnCollisionExitEvt;

        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            if (OnCollisionEnterEvt != null)
                OnCollisionEnterEvt(collision);
            CollisionEnter.Invoke();
        }

        private void OnCollisionExit(UnityEngine.Collision collision)
        {
            if (OnCollisionExitEvt != null)
                OnCollisionExitEvt(collision);
            CollisionExit.Invoke();
        }
    }
}