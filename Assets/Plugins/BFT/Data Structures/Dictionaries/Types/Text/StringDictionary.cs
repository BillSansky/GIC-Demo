using System;

namespace BFT
{
    [Serializable]
    public class StringDictionary : EntryDictionary<string, StringEntry>
    {
        public override StringEntry CreateNewDataHolder(object input)
        {
            return new StringEntry()
            {
                Data = (string) input
            };
        }
    }
}
