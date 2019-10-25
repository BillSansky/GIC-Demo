using UnityEngine;

namespace BFT
{
    public class OnDisableEvent : MonoBehaviour
    {
        public GameObjectEvent OnDisabled;

        private void OnDisable()
        {
            OnDisabled.Invoke(gameObject);
        }
    }
}
