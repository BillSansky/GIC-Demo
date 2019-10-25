using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

#endif

namespace BFT
{
    public class SingletonSO<T> : SerializedScriptableObject where T : ScriptableObject
    {
        // ReSharper disable once StaticMemberInGenericType
        protected static bool IsEditor;

        private static T instance;

        /// <summary>
        ///     Returns the instance of this singleton.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (!instance)
                {
                    SetReady();
#if UNITY_EDITOR
                    if (!instance)
                    {
                        UnityEngine.Debug.LogWarning("An instance of " + typeof(T) + " is needed");
                    }
#endif
                }

                return instance;
            }
        }

        public static bool IsReady => instance;

        public static bool IsRegistered => ManagerStructure.GetSingleton<T>();

        public static SingletonStructure ManagerStructure
        {
            get
            {
#if UNITY_EDITOR
                return IsEditor ? EditorSingletonManager.Instance.Structure : SingletonManager.Instance.Structure;
#else
            return SingletonManager.Instance.Structure;
#endif
            }
        }

        public static event System.Action OnInstanceReady;

        public static void SetReady()
        {
            T singleton = ManagerStructure.GetSingleton<T>();

            if (singleton == null)
            {
                T[] sings = Resources.FindObjectsOfTypeAll<T>();
                T newSing = sings != null && sings.Any() ? sings.First() : null;
                if (newSing == null)
                {
                    sings = Resources.LoadAll<T>("");
                    newSing = sings != null && sings.Any() ? sings.First() : null;
                }

                if (newSing == null)
                {
#if UNITY_EDITOR
                    UnityEngine.Debug.Log("The singleton does not exist! " +
                              "If in editor, it will be now created in the default BFT folder.");
                    newSing = CreateInstance<T>();

                    string path = IsEditor ? EditorSingletonManager.FullPath : SingletonManager.FullPath;

                    Directory.CreateDirectory(Application.dataPath + "/" + path);
                    UnityEditor.AssetDatabase.CreateAsset(newSing, "Assets/" + path + newSing.GetType().Name + ".asset");
#else
                Debug.LogError("The singleton does not exist! " + typeof(T).ToString());
#endif
                }

                ManagerStructure.AddSingleton(newSing.GetType(), newSing);
                instance = newSing;
            }
            else
            {
                instance = singleton;
            }

            instance.hideFlags = HideFlags.DontUnloadUnusedAsset;
            OnInstanceReady?.Invoke();
        }

        void Reset()
        {
            if (ManagerStructure.GetSingleton<T>())
            {
                UnityEngine.Debug.LogWarning("A singleton of this type has already been added! : "
                                 + ManagerStructure.GetSingleton<T>(),
                    ManagerStructure.GetSingleton<T>());
                UnityEngine.Debug.LogWarning(" This asset : " + this + "will not be registered", this);
            }

            ManagerStructure.AddSingleton(GetType(), this);
        }

        void OnDestroy()
        {
            ManagerStructure.RemoveSingleton(GetType());
        }

        [OnInspectorGUI]
        void OnGuiCheck()
        {
#if UNITY_EDITOR
            if (EditorApplication.isCompiling || EditorApplication.isUpdating)
                return;
#endif

            if (!ManagerStructure.ContainsSingleton(this))
            {
                if (!ManagerStructure.AddSingleton(GetType(), this))
                {
                    UnityEngine.Debug.LogWarning("Two singletons of type '" + GetType() + "'" +
                                     " were added, here...", this);
                    UnityEngine.Debug.LogWarning("...And Here: Remove one of them or it" +
                                     " will create unexpected behavior",
                        ManagerStructure.GetSingleton<T>());
                }
            }
        }
    }
}
