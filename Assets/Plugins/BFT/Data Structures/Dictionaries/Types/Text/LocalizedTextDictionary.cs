namespace BFT
{
    public class LocalizedTextDictionary : LocalizedDictionary<string, StringEntry>
    {
        public override StringEntry CreateNewDataHolder(object data)
        {
            return new StringEntry();
        }
    }
}
