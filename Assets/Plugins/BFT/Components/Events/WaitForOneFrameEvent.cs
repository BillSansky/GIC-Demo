using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    public class WaitForOneFrameEvent : MonoBehaviour
    {
        public UnityEvent OnOneFrameLater;
        public bool WaitForOneFrameOnEnable;

        public void OnEnable()
        {
            if (WaitForOneFrameOnEnable)
                WaitForOneFrame();
        }

        public void WaitForOneFrame()
        {
            StartCoroutine(TriggerAfterOneFrame());
        }

        private IEnumerator TriggerAfterOneFrame()
        {
            yield return new WaitForEndOfFrame();
            OnOneFrameLater.Invoke();
        }
    }
}
