using System;

namespace BFT
{
    [Serializable]
    public class FloatValue : GenericValue<float>
    {
        public FloatValue()
        {
        }

        public FloatValue(float localValue) : base(localValue)
        {
        }
    }
}
