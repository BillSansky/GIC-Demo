using System.Collections.Generic;
using BFT;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.BFT.Physics.Triggers.Trigger_Influences
{
    public abstract class TriggerInfluence : UpdateTypeSwitchable
    {
        [InfoBox("Is able to influence objects having an 'InfluenceableObject' component")]
        public static Dictionary<Collider, InfluenceableObject> InfluenceableObjectsByCollider
            = new Dictionary<Collider, InfluenceableObject>();

        private readonly List<InfluenceableObject> influenceableObjects = new List<InfluenceableObject>();

        public List<EnumAsset> LayersOfInfluence;

        private void OnTriggerEnter(Collider other)
        {
            if (InfluenceableObjectsByCollider.ContainsKey(other)
                && (LayersOfInfluence.Count == 0
                    || LayersOfInfluence.ContainsAny(InfluenceableObjectsByCollider[other].InfluenceableLayers)))
            {
                InfluenceableObject obj = InfluenceableObjectsByCollider[other];

                AddInfluenceableObject(obj);
            }
        }

        public virtual void AddInfluenceableObject(InfluenceableObject obj)
        {
            if (!influenceableObjects.Contains(obj))
            {
                influenceableObjects.Add(obj);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (InfluenceableObjectsByCollider.ContainsKey(other))
            {
                InfluenceableObject obj = InfluenceableObjectsByCollider[other];
                RemoveInfluenceableObject(obj);
            }
        }

        public virtual void RemoveInfluenceableObject(InfluenceableObject obj)
        {
            influenceableObjects.Remove(obj);
        }

        public override void UpdateMethod()
        {
            foreach (var influenceableObject in influenceableObjects)
            {
                Influence(influenceableObject);
            }
        }

        protected abstract void Influence(InfluenceableObject obj);
    }
}