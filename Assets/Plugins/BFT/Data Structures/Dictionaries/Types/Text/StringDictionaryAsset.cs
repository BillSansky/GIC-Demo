using UnityEngine;

namespace BFT
{
    [CreateAssetMenu(menuName = "BFT/Data/Data Holders/Text/Text Dictionary", fileName = "Text Dictionary")]
    public class StringDictionaryAsset : DictionaryAsset<string, StringEntry>
    {
        public override StringEntry CreateNewDataHolder(object data)
        {
            return new StringEntry();
        }
    }
}
