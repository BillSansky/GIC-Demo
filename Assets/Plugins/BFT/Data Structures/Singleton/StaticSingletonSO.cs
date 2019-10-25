using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class StaticSingletonSO<T> : SerializedScriptableObject where T : SerializedScriptableObject
    {
        // ReSharper disable once StaticMemberInGenericType
        public static string Path = "BFT/";

        // ReSharper disable once StaticMemberInGenericType
        public static string Name = "Static Singleton";

        private static T instance;

        public static string FullPath => "Resources/" + Path;

        public static T Instance
        {
            get
            {
                if (!instance)
                {
                    T[] arr = Resources.LoadAll<T>(Path);

                    instance = arr != null && arr.Any() ? arr.First() : null;
                    if (!instance)
                    {
#if UNITY_EDITOR

                        instance = CreateInstance<T>();
                        CreateSingleton();
#endif
                    }
                }

                return instance;
            }
        }


        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void OnDestroy()
        {
            instance = null;
        }


#if UNITY_EDITOR

        private static void CreateSingleton()
        {
            Directory.CreateDirectory(Application.dataPath + FullPath);
            UnityEditor.AssetDatabase.CreateAsset(instance, "Assets/Resources/" + Path + Name + ".asset");
        }

#endif
    }
}
