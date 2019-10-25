using System;

namespace BFT
{
    [Serializable]
    public class FloatVariable : GenericVariable<float>
    {
        public FloatVariable(float localValue) : base(localValue)
        {
            LocalValue = localValue;
        }
    }
}
