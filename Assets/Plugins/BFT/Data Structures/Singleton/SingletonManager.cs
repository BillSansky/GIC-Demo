using System;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
using Sirenix.Serialization;

namespace BFT
{
    [InitializeOnLoad]
#endif
    public class SingletonManager : StaticSingletonSO<SingletonManager>
    {
        [NonSerialized, OdinSerialize] public SingletonStructure Structure = new SingletonStructure();

        static SingletonManager()
        {
            Name = "Singleton Manager";
        }

        public T GetSingleton<T>() where T : ScriptableObject
        {
            return Structure.GetSingleton<T>();
        }

        public bool ContainsSingleton<T>(T singleton) where T : ScriptableObject
        {
            return Structure.ContainsSingleton(singleton);
        }

        public bool AddSingleton<T>(Type type, T singleton) where T : ScriptableObject
        {
            return Structure.AddSingleton(type, singleton);
        }

        public void RemoveSingleton(Type type)
        {
            Structure.RemoveSingleton(type);
        }
    }
}
