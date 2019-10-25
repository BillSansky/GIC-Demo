using System;

namespace BFT
{
    [Serializable]
    public class BoolVariableComponentDictionary : EntryDictionary<BoolVariableComponent, BoolVariableComponentEntry>
    {
        public override BoolVariableComponentEntry CreateNewDataHolder(object input)
        {
            return new BoolVariableComponentEntry() {Data = (BoolVariableComponent) input};
        }
    }
}
