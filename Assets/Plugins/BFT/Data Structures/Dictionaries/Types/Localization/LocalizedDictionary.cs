using UnityEngine.Serialization;

namespace BFT
{
    public abstract class LocalizedDictionary<T, T1> : DictionaryAsset<T, T1> where T1 : class, IDictionaryEntry<T>
    {
        [FormerlySerializedAs("localizedEntryDictionaries")] [FormerlySerializedAs("LocalizedDictionaries")]
        public DictionaryAsset<DictionaryAsset<T, T1>, DictionaryValueDictionaryEntry<T, T1>> localizedDictionaries;

        void OnValidate()
        {
            MasterKeys = LocalizationManager.Instance.languageKeysEntryDictionary;
        }

        public override T Get(int id)
        {
            if (LocalizationManager.Instance.CurrentLanguageID == 0)
            {
                return base[id];
            }

            return localizedDictionaries[LocalizationManager.Instance.CurrentLanguageID][id];
        }

        public override void Set(int id, T value)
        {
            if (LocalizationManager.Instance.CurrentLanguageID == 0)
            {
                base[id] = value;
            }

            localizedDictionaries[LocalizationManager.Instance.CurrentLanguageID][id] = value;
        }
    }
}
