using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    [Serializable]
    public class StringEntry : DictionaryEntry<string>
    {
        [Tooltip("The field for the english language")] [SerializeField, MultiLineProperty(5)]
        private string field;

        [SerializeField] private string id;

        public override string NameID
        {
            get => id;

            set => id = value;
        }

        public override string Data
        {
            get => field;

            set => field = value;
        }

        public override JsonData ExportJsonData()
        {
            JsonData data = new JsonData();
            data.DataByID.Add("Name ID", NameID);
            data.DataByID.Add("Value", field);

            return data;
        }

        public override void ParseJsonData(JsonData data)
        {
            NameID = data.DataByID["Name ID"];
            field = data.DataByID["Value"];
        }

        public override void NotifyJSonDataDeleteRequest()
        {
            //nothing to do
        }
    }
}
