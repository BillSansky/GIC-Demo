using System.Collections.Generic;
using BFT;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.BFT.Physics.Triggers.Trigger_Influences
{
    /// <summary>
    ///     An object tha6t can be influenced by a trigger influence
    /// </summary>
    public class InfluenceableObject : SerializedMonoBehaviour
    {
        public Collider ColliderForDetection;

        public List<EnumAsset> InfluenceableLayers;

        private void OnEnable()
        {
            TriggerInfluence.InfluenceableObjectsByCollider.Add(ColliderForDetection, this);
        }

        private void OnDisable()
        {
            TriggerInfluence.InfluenceableObjectsByCollider.Remove(ColliderForDetection);
        }
    }
}