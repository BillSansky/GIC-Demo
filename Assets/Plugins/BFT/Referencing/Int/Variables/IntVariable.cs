using System;

namespace BFT
{
    [Serializable]
    public class IntVariable : GenericVariable<int>, IValue<float>
    {
        public IntVariable(int value)
        {
            Value = value;
        }

        float IValue<float>.Value => Value;
    }
}
