using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace BFT
{
    [Serializable]
    public class SingletonStructure
    {
        [NonSerialized, OdinSerialize, ShowInInspector]
        private Dictionary<Type, ScriptableObject> singletons = new Dictionary<Type, ScriptableObject>();

        public T GetSingleton<T>() where T : ScriptableObject
        {
            Type type = typeof(T);
            if (singletons.ContainsKey(type))
                return (T) singletons[type];
            return null;
        }

        public bool ContainsSingleton<T>(T singleton) where T : ScriptableObject
        {
            return singletons.ContainsValue(singleton);
        }

        public bool AddSingleton<T>(Type type, T singleton) where T : ScriptableObject
        {
            if (singleton && (!singletons.ContainsKey(type) || singletons[type] == null))
            {
                singletons.AddOrReplace(type, singleton);
                return true;
            }

            return false;
        }

        public void RemoveSingleton(Type type)
        {
            singletons.Remove(type);
        }


#if UNITY_EDITOR

        [BoxGroup("Utils")] public ScriptableObject SingletonToAdd;

        private bool IsSingletonToAdd => singletons.ContainsValue(SingletonToAdd);

        [BoxGroup("Utils"), Button(ButtonSizes.Medium), HideIf("IsSingletonToAdd")]
        public void InsertSingleton()
        {
            if (SingletonToAdd)
            {
                AddSingleton(SingletonToAdd.GetType(), SingletonToAdd);
                SingletonToAdd = null;
            }
        }

        [BoxGroup("Utils"), Button(ButtonSizes.Medium), ShowIf("IsSingletonToAdd")]
        public void ReplaceSingleton()
        {
            if (SingletonToAdd)

            {
                singletons[SingletonToAdd.GetType()] = SingletonToAdd;
                SingletonToAdd = null;
            }
        }

        [Button(ButtonSizes.Medium), BoxGroup("Utils")]
        private void ResetSingletons()
        {
            foreach (KeyValuePair<Type, ScriptableObject> kvp in singletons)
            {
                var contextType = typeof(SingletonSO<>).MakeGenericType(kvp.Key);
                var methodInfo = contextType.GetMethod("SetReady");
                if (methodInfo != null) methodInfo.Invoke(null, null);
            }
        }
#endif
    }
}
