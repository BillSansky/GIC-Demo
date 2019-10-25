using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class SignalReceiverGroup : MonoBehaviour
    {
        public List<SignalReceiver> Receivers;

        private Dictionary<SignalType, SignalReceiver> receiversBySignalType;

        private void Awake()
        {
            receiversBySignalType = new Dictionary<SignalType, SignalReceiver>(Receivers.Count);

            foreach (var receiver in Receivers)
            {
                receiversBySignalType.Add(receiver.SignalType, receiver);
            }
        }

        public void ReceiveSignal(SignalType type)
        {
            receiversBySignalType[type].ReceiveSignal();
        }

        [Button(ButtonSizes.Medium)]
        public void AddAllReceiversOnObject(GameObject go)
        {
            foreach (var receiver in go.GetComponents<SignalReceiver>())
            {
                if (!Receivers.Contains(receiver))
                    Receivers.Add(receiver);
            }
        }
    }
}
