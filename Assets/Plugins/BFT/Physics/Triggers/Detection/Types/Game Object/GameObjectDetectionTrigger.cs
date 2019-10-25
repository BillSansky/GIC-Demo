using BFT;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.BFT.Physics.Triggers.Detection.Types.Game_Object
{
    public class GameObjectDetectionTrigger : AbstractDetectionTrigger<GameObject>
    {
        [BoxGroup("Detection", Order = -1)] public LayerMask CollisionLayer;

        [BoxGroup("Events")] public GameObjectEvent OnObjectTriggerEnter;

        [BoxGroup("Events")] public GameObjectEvent OnObjectTriggerExit;

        [ShowInInspector, BoxGroup("Status"), ReadOnly]
        public override GameObject LastDetectedObject { get; protected set; }

        [ShowInInspector, BoxGroup("Status"), ReadOnly]
        public override bool IsTriggered { get; protected set; }

        void OnTriggerEnter(Collider entering)
        {
            if (!enabled || (!CollisionLayer.IsInLayerMask(entering.gameObject) && CollisionLayer != 0))
                return;
            LastDetectedObject = entering.gameObject;
            IsTriggered = true;
            InvokeTriggerEntered();
            OnObjectTriggerEnter.Invoke(LastDetectedObject);
        }

        void OnTriggerExit(Collider exiting)
        {
            if (!enabled || (!CollisionLayer.IsInLayerMask(exiting.gameObject) && CollisionLayer != 0))
                return;
            LastDetectedObject = exiting.gameObject;
            IsTriggered = false;
            InvokeTriggerExited();
            OnObjectTriggerExit.Invoke(LastDetectedObject);
        }
    }
}