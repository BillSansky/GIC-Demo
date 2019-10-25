using System;

namespace BFT
{
    [Serializable]
    public class GameObjectEntry : ObjectEntry<UnityEngine.GameObject>
    {
        public override JsonData ExportJsonData()
        {
            throw new NotSupportedException();
        }

        public override void ParseJsonData(JsonData data)
        {
            throw new NotSupportedException();
        }

        public override void NotifyJSonDataDeleteRequest()
        {
            //nothing to do
        }
    }
}
