using Sirenix.OdinInspector;

namespace BFT
{
    public class BoolToIntValueComponent : SerializedMonoBehaviour,
        IValue<int>
    {
        public BoolValue BoolValue;
        public int FalseValue = 0;
        public int TrueValue = 1;

        public int IntValue => BoolValue.Value ? TrueValue : FalseValue;

        public int Value => IntValue;
    }
}
