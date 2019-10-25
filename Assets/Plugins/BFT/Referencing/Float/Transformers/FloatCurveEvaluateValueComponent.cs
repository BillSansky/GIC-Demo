using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class FloatCurveEvaluateValueComponent : MonoBehaviour, IValue<float>
    {
        public AnimationCurve InputPercentCurve;
        public FloatValue InputValue;

        [ShowInInspector] public float Value => InputPercentCurve.Evaluate(InputValue.Value);
    }
}
