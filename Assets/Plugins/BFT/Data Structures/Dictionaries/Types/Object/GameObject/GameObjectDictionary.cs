namespace BFT
{
    public class GameObjectDictionary : DictionaryAsset<UnityEngine.GameObject, GameObjectEntry>
    {
        public override GameObjectEntry CreateNewDataHolder(object data)
        {
            return new GameObjectEntry();
        }
    }
}
