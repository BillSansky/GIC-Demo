using System;

namespace BFT
{
    [Serializable]
    public class BoolDictionary : EntryDictionary<bool, BoolEntry>
    {
        public override BoolEntry CreateNewDataHolder(object input)
        {
            return new BoolEntry()
            {
                Data = (bool) input
            };
        }
    }
}
