using Sirenix.OdinInspector;

namespace BFT
{
    public class FloatToPercentValue : SerializedMonoBehaviour, IValue<float>
    {
        public IValue<float> FloatValue;
        public float MaxValue = 1;

        public float MinValue = -1;

        public float Value => MathExt.Percent(MinValue, MaxValue, FloatValue.Value);
    }
}
