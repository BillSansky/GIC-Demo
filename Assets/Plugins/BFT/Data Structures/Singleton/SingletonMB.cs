using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    /// <summary>
    ///     Generic Implementation of a Singleton MonoBehaviour.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonMB<T> : SerializedMonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public virtual void Awake()
        {
            if (instance)
            {
                if (instance == this)
                    return;
                UnityEngine.Debug.LogError("Two instances of the same singleton are present, Registered one is : " + instance.name,
                    instance);
                UnityEngine.Debug.LogError("Copy is : " + this, this);
            }
            else
                SetReady();
        }

        #region  Properties

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
                    if (!instance)
                    {
                        UnityEngine.Debug.LogWarning("An instance of " + typeof(T) + " is needed in the scene, but there is none.");
                    }
                }

                return instance;
            }
        }

        public static bool IsReady => instance;

        public static bool PreventAutoGeneration = false;

        public static event System.Action OnInstanceReady;

        public static bool IsReadyFunc()
        {
            return IsReady;
        }

        public static void SetReady()
        {
            instance = (T) FindObjectOfType(typeof(T));

            if (instance)
            {
                OnInstanceReady?.Invoke();
            }
            else if (!PreventAutoGeneration)
            {
                GameObject go = new GameObject("BFT Generated Singleton");
                instance = go.AddComponent<T>();
                OnInstanceReady?.Invoke();
            }
        }

        public static bool TrySetReady()
        {
            SetReady();
            return IsReady;
        }

        #endregion
    }
}
