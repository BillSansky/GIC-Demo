using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class IntToStringVariableComponent : MonoBehaviour, IValue<string>
    {
        [BoxGroup("Options")] public int DigitAmount = 1;

        [BoxGroup("Reference")] public IntValue IntValue;

        [BoxGroup("Options")] public int RoundToValue = 1;

        public string Value =>
            ((int) ((float) IntValue.Value / RoundToValue) * RoundToValue)
            .ToString("D" + DigitAmount);
    }
}
