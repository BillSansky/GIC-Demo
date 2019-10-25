using System;
using UnityEngine;

namespace BFT
{
    public static class CoroutineExt
    {
        public static void CallAfterSomeTime(this MonoBehaviour component, float time, System.Action action)
        {
            component.StartCoroutine(CoroutineUtils.CallAfterSomeTime(time, action));
        }


        public static void CallAfterOneFrame(this MonoBehaviour component, System.Action action)
        {
            component.StartCoroutine(CoroutineUtils.CallAfterOneFrame(action));
        }


        public static void CallActionWhenValueReady(this MonoBehaviour component, Func<bool> test, System.Action action)
        {
            component.StartCoroutine(CoroutineUtils.ActionWhenValueReady(test, action));
        }

        public static void CallEveryFrame(this MonoBehaviour component, System.Action action)
        {
            component.StartCoroutine(CoroutineUtils.CallEveryFrame(action));
        }

        public static void CallRegularly(this MonoBehaviour component, float timeInterval, System.Action action,
            bool waitRealTime = false)
        {
            component.StartCoroutine(CoroutineUtils.CallRegularly(timeInterval, action, waitRealTime));
        }
    }
}
