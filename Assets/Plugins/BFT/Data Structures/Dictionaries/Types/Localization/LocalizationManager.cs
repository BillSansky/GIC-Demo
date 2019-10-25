using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace BFT
{
    public class LocalizationManager : SingletonSO<LocalizationManager>
    {
        [ValueDropdown("LanguageDropdown"), SerializeField]
        private int currentLanguageID;

        [FormerlySerializedAs("LanguageKeysDictionary")]
        public IEntryDictionary<int> languageKeysEntryDictionary;

        public UnityEvent OnLanguageChanged;

        public ValueDropdownList<int> LanguageDropdown => languageKeysEntryDictionary.ContentIDs;

        public int CurrentLanguageID
        {
            get => currentLanguageID;
            set
            {
                currentLanguageID = value;
                OnLanguageChanged.Invoke();
            }
        }

        //TODO adapt to any kind of localized data,
        //TODO most likely reference to auto update the ID of all the localized dictionary
        /*   [SerializeField]
       private List<TextDictionary> localizedTextDictionaries=new List<TextDictionary>();

       public void AddLocalizedDictionary(TextDictionary dic)
       {
           localizedTextDictionaries.RemoveAll(_ => !_);
           if(!localizedTextDictionaries.Contains(dic))
               localizedTextDictionaries.Add(dic);
       }

       public bool IsDictionaryRegistered(TextDictionary dic)
       {
           return localizedTextDictionaries.Contains(dic);
       }

       public void PushLocalizationDataToSpreadsheet()
       {
           foreach (TextDictionary dictionary in localizedTextDictionaries)
           {
             //TODO do the logic of getting and retrieving the localized values
           }
       }*/
    }
}
