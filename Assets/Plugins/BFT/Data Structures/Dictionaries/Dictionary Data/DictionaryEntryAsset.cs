using System.IO;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
#endif

namespace BFT
{
    /// <summary>
    ///     A useful class when a dictionary needs to use other assets as data:
    ///     this way the ID of the asset is maintained when it is changed,
    ///     and the asset can use its own ID internally for other dictionary referenced fields.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DictionaryEntryAsset<T> : SerializedScriptableObject, IDictionaryEntry<T>
        where T : SerializedScriptableObject
    {
        public static readonly string NameIDJsonID = "Name ID";

        [SerializeField, BoxGroup("Tools/Referencing")]
        private int dictionaryID = -1;

        [BoxGroup("Tools", order: 99999)] public bool LogDebug = false;

        public int ID => dictionaryID;

        /// <summary>
        ///     should return self for a proper usage
        /// </summary>
        public abstract T Data { get; set; }


        /// <summary>
        ///     Will be used by the dictionary to refresh the value of the ID so that the asset still references to itself in the
        ///     dictionary.
        /// </summary>
        /// <param name="newID"></param>
        public virtual void RefreshDictionaryKey(int newID)
        {
            if (LogDebug)
                UnityEngine.Debug.Log("Ref " + name + " from: " + dictionaryID + " to: " + newID);
            dictionaryID = newID;

#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        public string NameID
        {
            get => name;

            set => name = value;
        }

        public virtual JsonData ExportJsonData()
        {
            JsonData data = new JsonData();
            data.DataByID.Add(NameIDJsonID, name);
            return data;
        }

        public virtual void ParseJsonData(JsonData data)
        {
#if UNITY_EDITOR

            string newName = data.DataByID[NameIDJsonID];
            if (this && name != data.DataByID[NameIDJsonID])
            {
                if (LogDebug)
                    UnityEngine.Debug.Log("Renaming Asset " + name + " into " + newName);

                UnityEditor.EditorUtility.SetDirty(this);

                string path = UnityEditor.AssetDatabase.GetAssetPath(this);

                if (path == null)
                {
                    if (LogDebug)
                        UnityEngine.Debug.LogWarning("The object's path was not found! The json parsing cannot happen", this);
                    return;
                }

                UnityEditor.AssetDatabase.RenameAsset(path, newName);
                name = newName;
            }
            else if (Path.GetFileName(AssetDatabase.GetAssetPath(this)) != name)
            {
                UnityEditor.AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(this), newName);
            }

            dictionaryID = data.ID;
            //setting dirty again just in case
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        public virtual void NotifyJSonDataDeleteRequest()
        {
        }

        protected void SetDebugMessage(T value)
        {
            UnityEngine.Debug.LogWarningFormat(this,
                "trying to set a DataAsset to value {0}, but this is not allowed", value);
        }
    }
}
