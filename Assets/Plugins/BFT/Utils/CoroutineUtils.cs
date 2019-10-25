using System;
using System.Collections;
using UnityEngine;

namespace BFT
{
    public static class CoroutineUtils
    {
        public static IEnumerator CallEveryFrame(System.Action action)
        {
            while (true)
            {
                action();
                yield return null;
            }
        }

        public static IEnumerator CallRegularly(float seconds, System.Action action, bool waitRealTime = false)
        {
            while (true)
            {
                action();
                if (waitRealTime)
                {
                    yield return new WaitForSecondsRealtime(seconds);
                }
                else
                {
                    yield return new WaitForSeconds(seconds);
                }
            }
        }

        public static IEnumerator ActionWhenValueReady(Func<bool> test, System.Action action)
        {
            while (!test())
            {
                yield return null;
            }

            action();
        }

        public static IEnumerator CallAfterOneFrame(System.Action action)
        {
            yield return null;
            action();
        }

        public static IEnumerator CallAfterSomeTime(float time, System.Action action)
        {
            yield return new WaitForSeconds(time);
            action();
        }
    }
}
