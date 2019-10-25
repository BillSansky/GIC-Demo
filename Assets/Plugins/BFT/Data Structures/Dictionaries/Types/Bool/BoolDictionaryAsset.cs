using UnityEngine;

namespace BFT
{
    [CreateAssetMenu(menuName = "BFT/Data/Data Holders/Bool/Bool Dictionary", fileName = "Bool Dictionary")]
    public class BoolDictionaryAsset : DictionaryAsset<bool, BoolEntry>
    {
        public override BoolEntry CreateNewDataHolder(object data)
        {
            return new BoolEntry();
        }
    }
}
