namespace BFT
{
    public interface IJSonDataConverter
    {
        JsonData ExportJsonData();

        void ParseJsonData(JsonData data);

        void NotifyJSonDataDeleteRequest();
    }
}
