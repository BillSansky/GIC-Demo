using System;
using UnityEngine;

namespace BFT
{
    [Serializable]
    public class SpriteEntry : DictionaryEntry<UnityEngine.Sprite>
    {
        [SerializeField] private UnityEngine.Sprite field;

        [SerializeField] private string id;

        public override string NameID
        {
            get => id;

            set => id = value;
        }

        public override UnityEngine.Sprite Data
        {
            get => field;

            set => field = value;
        }

        public override JsonData ExportJsonData()
        {
            JsonData data = new JsonData();
            data.DataByID.Add("Name ID", NameID);
            data.DataByID.Add("Value", field.name);


            return data;
        }

        public override void ParseJsonData(JsonData data)
        {
            NameID = data.DataByID["Name ID"];

            UnityEngine.Sprite sprite =
                JSonDataUtils.GetAssetReferenceFromName<UnityEngine.Sprite>(data.DataByID["Value"]);

            if (sprite)
                field = sprite;
        }

        public override void NotifyJSonDataDeleteRequest()
        {
        }
    }
}
