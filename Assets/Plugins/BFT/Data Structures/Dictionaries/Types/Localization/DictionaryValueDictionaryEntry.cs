using UnityEngine;

namespace BFT
{
    /// <summary>
    ///     Used for nested dictionaries, useful for localization
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="T1"></typeparam>
    public class DictionaryValueDictionaryEntry<T, T1> : DictionaryEntry<DictionaryAsset<T, T1>>
        where T1 : class, IDictionaryEntry<T>
    {
        [SerializeField] private DictionaryAsset<T, T1> data;

        public override string NameID
        {
            get => Data.name;
            set => Data.name = value;
        }

        public override DictionaryAsset<T, T1> Data
        {
            get => data;
            set => data = value;
        }

        public override JsonData ExportJsonData()
        {
            throw new System.NotImplementedException();
        }

        public override void ParseJsonData(JsonData data)
        {
            throw new System.NotImplementedException();
        }

        public override void NotifyJSonDataDeleteRequest()
        {
            throw new System.NotImplementedException();
        }
    }
}
