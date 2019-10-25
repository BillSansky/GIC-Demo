using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class FloatToStringVariableComponent : MonoBehaviour, IValue<string>
    {
        [BoxGroup("Options")] public int DigitAmount = 1;

        [BoxGroup("Reference")] public FloatValue FloatValue;

        [BoxGroup("Options")] public int RoundToValue = 1;

        [ShowInInspector]
        public string Value => (((float) FloatValue.Value / RoundToValue) * RoundToValue).ToString("N" + DigitAmount);
    }
}
