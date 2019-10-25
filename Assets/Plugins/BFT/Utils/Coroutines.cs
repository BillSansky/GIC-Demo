using System.Collections;
using UnityEngine;

namespace BFT
{
    public interface ICoroutines
    {
        void Begin(IEnumerator coroutine);
        void Stop(IEnumerator coroutine);
        void StartDelayed(float delay, Action callback);
    }

    public class Coroutines : MonoBehaviour, ICoroutines
    {
        protected static Coroutines instance;

        public static ICoroutines Instance
        {
            get
            {
                if (instance != null)
                    return instance;
                var obj = new GameObject("Coroutines").AddComponent<Coroutines>();
                instance = obj;
                DontDestroyOnLoad(obj);
                return obj;
            }
        }

        public void Begin(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }

        public void Stop(IEnumerator coroutine)
        {
            StopCoroutine(coroutine);
        }

        public void StartDelayed(float delay, Action callback)
        {
            throw new System.NotImplementedException();
        }

        public void StartDelayed(float delay, System.Action callback)
        {
            ((MonoBehaviour) this).StartDelayed(delay, callback);
        }
    }
}
