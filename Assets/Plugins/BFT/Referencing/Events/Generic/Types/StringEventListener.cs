namespace BFT
{
    public class StringEventListener : GenericEventListener<string, StringEvent>
    {
        public StringEventAsset StringEvent;
        public override GenericEventAsset<string, StringEvent> Event => StringEvent;
    }
}
