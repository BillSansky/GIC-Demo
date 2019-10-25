using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    [Serializable]
    [InlineProperty]
    [HideReferenceObjectPicker]
    public class BoolEntry : DictionaryEntry<bool>
    {
        [SerializeField] private bool field;
        [SerializeField] private string id;

        public override string NameID
        {
            get => id;

            set => id = value;
        }

        public override bool Data
        {
            get => field;

            set => field = value;
        }

        public override JsonData ExportJsonData()
        {
            JsonData data = new JsonData();
            data.DataByID.Add("Name ID", NameID);
            data.DataByID.Add("Value", field.ToString());

            return data;
        }

        public override void ParseJsonData(JsonData data)
        {
            NameID = data.DataByID["Name ID"];
            field = bool.Parse(data.DataByID["Value"]);
        }

        public override void NotifyJSonDataDeleteRequest()
        {
            //nothing to do
        }
    }
}
