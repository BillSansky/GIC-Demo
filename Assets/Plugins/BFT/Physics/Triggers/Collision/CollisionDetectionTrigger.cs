using BFT;
using Plugins.BFT.Physics.Triggers.Detection;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.BFT.Physics.Triggers.Collision
{
    /// <summary>
    ///     Same as a detection trigger, but uses the collision callback
    /// </summary>
    public class CollisionDetectionTrigger : AbstractDetectionTrigger<UnityEngine.Collision>
    {
        private bool allowCollisions = true;
        [OnValueChanged("TryToSetRigidbody")] public bool IgnoreCollisionsOnKinematicChanges;
        public LayerMask LayersToCheck;
        private bool? prevKinematic;

        [SerializeField, ShowIf("IgnoreCollisionsOnKinematicChanges")]
        private Rigidbody relatedRigidbody;


        public override UnityEngine.Collision LastDetectedObject { get; protected set; }
        [ShowInInspector, ReadOnly] public override bool IsTriggered { get; protected set; }

        private void TryToSetRigidbody()
        {
            if (IgnoreCollisionsOnKinematicChanges && relatedRigidbody == null)
            {
                relatedRigidbody = GetComponent<Rigidbody>();
            }
        }

        private void LateUpdate()
        {
            if (IgnoreCollisionsOnKinematicChanges)
            {
                allowCollisions = !prevKinematic.HasValue || relatedRigidbody.isKinematic == prevKinematic;
                prevKinematic = relatedRigidbody.isKinematic;
            }
        }

        private void OnCollisionEnter(UnityEngine.Collision other)
        {
            if (LogTrigger)
            {
                UnityEngine.Debug.Log(
                    "Collision with: " + LayerMask.LayerToName(other.collider.gameObject.layer) + " (Is in mask: " +
                    PhysicsTools.IsLayerInMask(LayersToCheck, other.collider.gameObject.layer) + ")", other.gameObject);
            }

            if (!allowCollisions)
                return;

            if (PhysicsTools.IsLayerInMask(LayersToCheck, other.collider.gameObject.layer))
            {
                LastDetectedObject = other;
                IsTriggered = true;
                InvokeTriggerEntered();
            }
        }

        private void OnCollisionExit(UnityEngine.Collision other)
        {
            if (!allowCollisions)
                return;

            if (PhysicsTools.IsLayerInMask(LayersToCheck, other.collider.gameObject.layer))
            {
                LastDetectedObject = other;
                IsTriggered = false;
                InvokeTriggerExited();
            }
        }
    }
}