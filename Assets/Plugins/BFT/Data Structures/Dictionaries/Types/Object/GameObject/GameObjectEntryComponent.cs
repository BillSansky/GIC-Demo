using System;

namespace BFT
{
    public class GameObjectEntryComponent : DictionaryEntryComponent<UnityEngine.GameObject>
    {
        public override UnityEngine.GameObject Data
        {
            get => gameObject;
            set
            {
                //not authorized
            }
        }

        public override JsonData ExportJsonData()
        {
            throw new NotImplementedException();
        }

        public override void ParseJsonData(JsonData data)
        {
            throw new NotImplementedException();
        }

        public override void NotifyJSonDataDeleteRequest()
        {
            throw new NotImplementedException();
        }
    }
}
