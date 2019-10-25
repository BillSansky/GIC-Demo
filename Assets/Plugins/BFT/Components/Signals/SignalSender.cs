using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class SignalSender : MonoBehaviour
    {
        public SignalType SignalType;

        [InfoBox("Ideally, try to have a Signal Receiver or a signal receiver group directly on the object called," +
                 " and a SignalReceiverGroup whenever possible")]
        public void SendSignal(GameObject target)
        {
            //first check for Group on the object
            var group = target.GetComponent<SignalReceiverGroup>();

            if (!group)
            {
                //then try to find normal receives on the object
                var receivers = target.GetComponents<SignalReceiver>();
                bool found = false;
                foreach (var receiver in receivers)
                {
                    if (receiver.SignalType == SignalType)
                    {
                        receiver.ReceiveSignal();
                        found = true;
                    }
                }

                if (found)
                    return;
            }

            if (!group)
                group = target.GetComponentInChildren<SignalReceiverGroup>();

            if (group)
            {
                group.ReceiveSignal(SignalType);
                return;
            }

            var allReceivers = target.GetComponentsInChildren<SignalReceiver>();

            foreach (var receiver in allReceivers)
            {
                if (receiver.SignalType == SignalType)
                {
                    receiver.ReceiveSignal();
                }
            }
        }

        public void SendSignalToAllChildren(GameObject go)
        {
            go.ForEachChildrenGameObject(SendSignal);
        }

        public void SendSignal(GameObjectReferenceAsset reference)
        {
            SendSignal(reference.Value);
        }
    }
}
