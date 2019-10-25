using System;
using Sirenix.Serialization;
using UnityEngine;

#if UNITY_EDITOR
namespace BFT
{
    [UnityEditor.InitializeOnLoad]
#endif
    public class EditorSingletonManager : StaticSingletonSO<EditorSingletonManager>
    {
        [NonSerialized, OdinSerialize] public SingletonStructure Structure = new SingletonStructure();

        static EditorSingletonManager()
        {
            Name = "Editor Singleton Manager";
            Path = "Editor/BFT/";
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
