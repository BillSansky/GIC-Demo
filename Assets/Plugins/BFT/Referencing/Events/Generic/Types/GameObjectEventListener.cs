namespace BFT
{
    public class GameObjectEventListener : GenericEventListener<UnityEngine.GameObject, GameObjectEvent>
    {
        public GameObjectEventAsset GameObjectEvent;
        public override GenericEventAsset<UnityEngine.GameObject, GameObjectEvent> Event => GameObjectEvent;
    }
}
