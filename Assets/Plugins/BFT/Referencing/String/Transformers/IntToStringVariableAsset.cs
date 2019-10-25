using Sirenix.OdinInspector;

namespace BFT
{
    public class IntToStringVariableAsset : SerializedScriptableObject, IValue<string>
    {
        [BoxGroup("Options")] public int DigitAmount = 1;
        [BoxGroup("Reference")] public IValue<int> IntValue;

        public string Value => IntValue.Value.ToString("D" + DigitAmount);
    }
}
