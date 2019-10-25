using System.Collections.Generic;
using BFT;
using Sirenix.OdinInspector;
using UnityEngine;

//adds object to an internal set if the angle of collision is lower than a specified value
namespace Plugins.BFT.Physics.Triggers.Collision
{
    public class CollisionAngleDetection : MonoBehaviour, IValue<bool>
    {
        private readonly HashSet<GameObject> currentCollisions = new HashSet<GameObject>();

        [InfoBox("adds object to an internal set if the angle of collision is lower than a specified value")]
        public LayerMask LayersToCheck;

        public float MaxAngle;

        public bool Value => currentCollisions.Count > 0;

        protected void CheckAngleOnEnter(UnityEngine.Collision other)
        {
            if (PhysicsTools.IsLayerInMask(LayersToCheck, other.gameObject.layer))
            {
                float angle = Vector3.Angle(Vector3.up, other.contacts[0].normal);
                if (angle <= MaxAngle && !currentCollisions.Contains(other.gameObject))
                    currentCollisions.Add(other.gameObject);
            }
        }

        protected void CheckAngleOnExit(UnityEngine.Collision other)
        {
            if (PhysicsTools.IsLayerInMask(LayersToCheck, other.gameObject.layer))
            {
                if (currentCollisions.Contains(other.gameObject))
                    currentCollisions.Remove(other.gameObject);
            }
        }

        private void OnCollisionEnter(UnityEngine.Collision other)
        {
            CheckAngleOnEnter(other);
        }

        private void OnCollisionExit(UnityEngine.Collision other)
        {
            CheckAngleOnExit(other);
        }
    }
}