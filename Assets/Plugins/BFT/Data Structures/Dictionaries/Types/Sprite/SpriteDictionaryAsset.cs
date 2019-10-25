using UnityEngine;

namespace BFT
{
    [CreateAssetMenu(menuName = "BFT/Data/Data Holders/Sprite/Srptei Dictionary", fileName = "Sprite Dictionary")]
    public class SpriteDictionaryAsset : DictionaryAsset<UnityEngine.Sprite, SpriteEntry>
    {
        public override SpriteEntry CreateNewDataHolder(object data)
        {
            return new SpriteEntry();
        }
    }
}
