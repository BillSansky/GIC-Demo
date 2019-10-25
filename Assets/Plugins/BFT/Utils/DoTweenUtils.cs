using System;
using UnityEngine;

namespace BFT
{
    public static class DoTweenUtils
    {
        public static float SpringEasing(float time, float duration, float overshootoramplitude, float period)
        {
            return Spring(0, 1, time / duration);
        }

        public static float SpringEasingStrong(float time, float duration, float overshootoramplitude, float period)
        {
            return SpringStrong(0, 1, time / duration);
        }

        private static float Spring(float start, float end, float value)
        {
            value = Mathf.Clamp01(value);
            value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) +
                     value) * (1f + (1.2f * (1f - value)));
            return start + (end - start) * value;
        }

        private static float SpringStrong(float start, float end, float value)
        {
            value = Mathf.Clamp01(value);
            var e = Math.E;
            var result = (float) (-0.5 * Math.Pow(e, -6 * value) *
                                  (-2 * Math.Pow(e, (6 * value)) + Math.Sin(12 * value) + 2 * Math.Cos(12 * value)));
            return start + (end - start) * result;
        }
    }
}
