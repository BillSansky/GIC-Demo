using UnityEngine;

namespace BFT
{
    public static class PercentUtil
    {
        public static float CalculateFactor(float minFactor, float maxFactor, AnimationCurve profile,
            IValue<float> percentGiver)
        {
            return MathExt.EvaluateCurve(minFactor, maxFactor, profile, percentGiver.Value);
        }
    }
}
