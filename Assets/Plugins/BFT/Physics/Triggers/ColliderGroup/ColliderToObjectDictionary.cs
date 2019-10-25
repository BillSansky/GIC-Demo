using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Plugins.BFT.Physics.Triggers.ColliderGroup
{
    public class ColliderToObjectDictionary : SerializedScriptableObject
    {
        [OdinSerialize] private Dictionary<Collider, UnityEngine.Object> colliderToObject = new Dictionary<Collider, UnityEngine.Object>();

        public UnityEngine.Object GetObject(Collider collider)
        {
            UnityEngine.Object outT;
            colliderToObject.TryGetValue(collider, out outT);
            return outT;
        }

        public void AddObject(Collider collider, UnityEngine.Object o)
        {
            UnityEngine.Debug.AssertFormat(!colliderToObject.ContainsKey(collider),
                this, "The collider {0} is already associated to {1}", collider,
                colliderToObject[collider]);
            colliderToObject.Add(collider, o);
        }
    }
}