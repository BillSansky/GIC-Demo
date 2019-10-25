using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class GradientToColorComponent : MonoBehaviour, IValue<UnityEngine.Color>
    {
        public UnityEngine.Gradient Gradient;

        [HideIf("UseInNestedPrefab")] public PercentValue Percent;

        [ShowIf("UseInNestedPrefab")] public MultiTypeFloatValue PercentComponent;

        public bool UseInNestedPrefab;

        public UnityEngine.Color Value => Gradient.Evaluate(UseInNestedPrefab ? PercentComponent.Value : Percent.Value);
    }
}
