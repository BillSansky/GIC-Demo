namespace BFT
{
    public class TransformDictionary : DictionaryAsset<UnityEngine.Transform, TransformEntry>
    {
        public override TransformEntry CreateNewDataHolder(object data)
        {
            return new TransformEntry();
        }
    }
}
