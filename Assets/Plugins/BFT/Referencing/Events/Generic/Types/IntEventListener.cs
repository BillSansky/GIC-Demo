namespace BFT
{
    public class IntEventListener : GenericEventListener<int, IntEvent>
    {
        public IntEventAsset IntEvent;
        public override GenericEventAsset<int, IntEvent> Event => IntEvent;
    }
}
