using System;

namespace BFT
{
    [Serializable]
    public class BoolVariable : GenericVariable<bool>
    {
        public BoolVariable(bool value)
        {
            UseReference = false;
            LocalValue = value;
        }
    }
}
