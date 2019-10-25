using System.Collections;
using UnityEngine;

namespace BFT
{
    public static class MonoBehaviourExt
    {
        public static void StartDelayed(this MonoBehaviour obj, float delay, System.Action action)
        {
            obj.StartCoroutine(Delay(delay, action));
        }

        public static IEnumerator Delay(float delay, System.Action action)
        {
            yield return new WaitForSeconds(delay);
            if (action != null) action();
        }

        public static T SetObjectAs<T, T1>(this MonoBehaviour beh, T1 comp) where T : class where T1 : UnityEngine.Object
        {
            UnityEngine.Debug.Assert(comp is T,
                $"The component {comp.name} could not be casted to {typeof(T).Name} so the value will not be pasted correctly on {beh.name}",
                beh);
            return comp as T;
        }
    }
}
