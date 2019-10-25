using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class FitFloatValueComponent : MonoBehaviour, IValue<float>
    {
        [ShowIf("UseInputCurve")] public AnimationCurve InputPercentCurve;
        public FloatValue InputValue;
        public float MaxInput;
        public float MaxOutput;
        public float MinInput;

        public float MinOuput;

        public bool UseInputCurve;

        private bool IsInputDefined => InputValue != null;

        [ShowInInspector, ShowIf("IsInputDefined"), LabelText("Value")]
        public float EditorValue => IsInputDefined ? Value : 0;

        public float Value => UseInputCurve
            ? MathExt.Fit(InputValue.Value, MinInput, MaxInput, MinOuput, MaxOutput, InputPercentCurve)
            : MathExt.Fit(InputValue.Value, MinInput, MaxInput, MinOuput, MaxOutput);
    }
}
