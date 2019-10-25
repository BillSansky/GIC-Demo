using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    /// <summary>
    ///     Can be used to delegate the execution of something until it is compelted, in which case a specific action will be
    ///     called.
    /// </summary>
    public class RequestNotify : MonoBehaviour
    {
        [ShowIf("@AutoCompleteRequest")] public bool AutoCompleteAfterSomeTime = false;
        public bool AutoCompleteRequest = false;

        [ShowIf("@AutoCompleteRequest && AutoCompleteAfterSomeTime")]
        public float CompletionTime;

        private Action currentCallBack;
        public UnityEvent OnRequestCompleted;

        public UnityEvent OnRequestReceived;

        public bool IsBusy => currentCallBack != null;

        public void Request()
        {
            Request(null);
        }

        public void Request(Action callBack)
        {
            UnityEngine.Debug.Assert(currentCallBack == null,
                "A Request was already done, please wait until it complete before requesting again");

            currentCallBack = callBack;
            OnRequestReceived.Invoke();

            if (AutoCompleteRequest)
            {
                if (AutoCompleteAfterSomeTime)
                {
                    this.CallAfterSomeTime(CompletionTime, CompleteRequest);
                }
                else
                {
                    CompleteRequest();
                }
            }
        }

        public void CompleteRequest()
        {
            currentCallBack?.Invoke();
            currentCallBack = null;
            OnRequestCompleted.Invoke();
        }
    }
}
