using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    public class SignalReceiver : MonoBehaviour
    {
        public UnityEvent OnSignalReceived;
        public SignalType SignalType;

        public void ReceiveSignal()
        {
            OnSignalReceived.Invoke();
        }
    }
}
