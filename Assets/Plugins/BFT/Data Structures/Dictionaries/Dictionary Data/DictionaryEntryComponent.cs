using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

#endif

namespace BFT
{
    /// <summary>
    ///     An Entry for an EntryDictionary, but with the entry as a component
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DictionaryEntryComponent<T> : MonoBehaviour, IDictionaryEntry<T> where T : UnityEngine.Object
    {
        [SerializeField, ReadOnly] private int dictionaryID = -1;

        public int ID => dictionaryID;

        /// <summary>
        ///     To implement as a return to self
        /// </summary>
        public abstract T Data { get; set; }

        public void RefreshDictionaryKey(int newID)
        {
            dictionaryID = newID;

#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        public string NameID
        {
            get
            {
#if UNITY_EDITOR
                if (!gameObject)
                    return "";
#endif
                return name;
            }

            set { name = value; }
        }

        public abstract JsonData ExportJsonData();
        public abstract void ParseJsonData(JsonData data);
        public abstract void NotifyJSonDataDeleteRequest();

        protected void SetDebugMessage(T value)
        {
            UnityEngine.Debug.LogWarningFormat(this,
                "trying to set a DataComponent to value {0}, but this is not allowed", value);
        }

        [FoldoutGroup("Tools"), Button(ButtonSizes.Medium)]
        protected virtual void AttachToDictionary(IEntryDictionary<T> entryDictionary)
        {
            entryDictionary.Add(Data);
        }
    }
}
