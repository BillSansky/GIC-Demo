using System;

namespace BFT
{
    [Serializable]
    public class SpriteDictionary : EntryDictionary<UnityEngine.Sprite, SpriteEntry>
    {
        public override SpriteEntry CreateNewDataHolder(object input)
        {
            return new SpriteEntry()
            {
                Data = (UnityEngine.Sprite) input
            };
        }
    }
}
