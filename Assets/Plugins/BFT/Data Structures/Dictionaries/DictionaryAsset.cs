using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

#if UNITY_EDITOR
#endif

namespace BFT
{
    /// <summary>
    ///     The basic implementation of the entry dictionary interface as an asset.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public abstract class DictionaryAsset<T, T2> : SerializedScriptableObject,
        IEntryDictionary<T>, ISpreadSheetParser, IValue<IEntryDictionary<T>>
        where T2 : class, IDictionaryEntry<T>
    {
        
        /// <summary>
        ///     A base class for a quick implementation of the interface:
        ///     represents a value for a key in a dictionary.
        ///     has a name id for a better user interface recognition
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        [Serializable]
        [HideLabel]
        [InlineProperty]
        public abstract class DictionaryEntry<T1> : IDictionaryEntry<T1>
        {
            [SerializeField, HideInInspector] private int dictionaryID;

            public int ID
            {
                get => dictionaryID;

                private set => dictionaryID = value;
            }

            public void RefreshDictionaryKey(int newID)
            {
                dictionaryID = newID;
            }

            public abstract string NameID { get; set; }
            public abstract T1 Data { get; set; }

            public abstract JsonData ExportJsonData();
            public abstract void ParseJsonData(JsonData data);
            public abstract void NotifyJSonDataDeleteRequest();
        }
        
        [NonSerialized, OdinSerialize, ShowInInspector, OnValueChanged("RefreshAllEntries")]
        protected Dictionary<int, T2> DataByID = new Dictionary<int, T2>();

        [FoldoutGroup("Master Dictionary", false, 90)]
        [Tooltip(
            "If the dictionary is a master dictionary, it will refresh the keys of its member when the get added to the dictionary")]
        public bool IsMasterDictionary = true;

        [FoldoutGroup("Tools", false, 99999)] public bool LogDebug;

        [SerializeField, FoldoutGroup("Master Dictionary", false, 90)]
        public IIDsHolder MasterKeys;

        [FoldoutGroup("Events", false)] public UnityEvent OnDataAdded;

        [FormerlySerializedAs("ParentDictionary")] [SerializeField, FoldoutGroup("Master Dictionary")]
        public IEntryDictionary<T> parentEntryDictionary;

        private int? FirstAvailable =>
            Enumerable.Range(0, int.MaxValue).Except(DataByID.Keys).FirstOrDefault();

        public IEnumerable<int> Keys => DataByID.Keys;

        public bool ContainsKey(int id)
        {
            if (parentEntryDictionary == null)
            {
                return DataByID.ContainsKey(id);
            }

            return DataByID.ContainsKey(id) || parentEntryDictionary.ContainsKey(id);
        }

        public virtual T Get(int id)
        {
            if (LogDebug)
            {
                UnityEngine.Debug.LogFormat(this, "Dictionary {1} Gettting data for id {0}", id, name);
            }

            if (DataByID.ContainsKey(id))
            {
                UnityEngine.Debug.Assert(DataByID[id] != null, "The data you are trying to access is null", this);

                return DataByID[id].Data;
            }

            if (parentEntryDictionary != null)
            {
                return parentEntryDictionary.Get(id);
            }

            UnityEngine.Debug.LogWarning("The key was not found in the dictionary, returning default value", this);

            return default(T);
        }

        public T this[int index]
        {
            get => Get(index);
            set => Set(index, value);
        }

        public virtual void Set(int id, T value)
        {
            if (LogDebug)
            {
                UnityEngine.Debug.LogFormat(this, "Dictionary {2} setting data {1} for id {0}", id, value, name);
            }

            if (DataByID.ContainsKey(id))
            {
                DataByID[id].Data = value;
            }
            else
            {
                T2 newT2 = CreateNewDataHolder(null);
                newT2.Data = value;
                if (newT2.NameID.IsNullOrEmpty())
                {
                    newT2.NameID = value.ToString();
                }

                AddData(newT2, id);
            }

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        public virtual int Add(T data)
        {
            var variable = data as T2;
            if (variable != null)
            {
                return AddDataHolder(variable);
            }

            int? first = FirstAvailable;
            if (first != null)
            {
                int id = (int) first;
                Set(id, data);
                return (int) first;
            }

#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif

            return -1;
        }

        public void Remove(int id)
        {
            DataByID.Remove(id);

#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        public IEnumerator<int> GetEnumerator()
        {
            foreach (var value in DataByID.Keys)
            {
                yield return value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual void AfterParsePass()
        {
        }

        public IEntryDictionary<T> Value => this;

        [FoldoutGroup("Tools", false, 99999), Button(ButtonSizes.Medium), ShowIf("IsMasterDictionary")]
        private void RefreshAllEntries()
        {
            if (!IsMasterDictionary)
            {
                return;
            }

            foreach (var pair in DataByID)
            {
                if (pair.Value.ID == pair.Key)
                {
                    continue;
                }

                pair.Value.RefreshDictionaryKey(pair.Key);
            }
        }

        public T2 GetDataByName(string name)
        {
            return DataByID.FirstOrDefault(_ => _.Value.NameID == name).Value;
        }

        public abstract T2 CreateNewDataHolder(object input);

        /// <summary>
        ///     warning, costly call
        /// </summary>
        /// <param name="data"></param>
        public int GetFirstID(T value)
        {
            foreach (var data in DataByID)
            {
                if (data.Value != null && data.Value.Data.Equals(value))
                {
                    return data.Key;
                }
            }

            return -1;
        }


        /// <summary>
        ///     Warning: costly call!
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ContainsValue(T value)
        {
            foreach (var data in DataByID)
            {
                if (data.Value != null && data.Value.Data.Equals(value))
                {
                    return true;
                }
            }

            return false;
        }

        public bool ContainsValueHolder(T2 value)
        {
            foreach (var data in DataByID)
            {
                if (data.Value == value)
                {
                    return true;
                }
            }

            return false;
        }


        public void Clear()
        {
            DataByID.Clear();
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        public bool GetNotNullValue(out T2 data)
        {
            if (DataByID.Values.Any(_ => !_.IsNull()))
            {
                data = DataByID.Values.First(_ => !_.IsNull());
                return true;
            }

            data = null;

            return false;
        }

        public virtual int AddDataHolder(T2 entry)
        {
            return AddData(entry, null);
        }

        public virtual void RemoveData(T2 entry)
        {
            foreach (var kvp in DataByID)
            {
                if (kvp.Value == entry)
                {
                    DataByID.Remove(kvp.Key);
                    break;
                }
            }
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }


        public int AddData(T2 entry, int? id)
        {
            if (id == null)
            {
                id = FirstAvailable;
            }

            if (id == null)
            {
                return -1;
            }

            if (LogDebug)
            {
                UnityEngine.Debug.LogFormat(this, "Dictionary {2} adding data {1} for id {0}", id, entry, name);
            }

            DataByID.Add((int) id, entry);

            if (IsMasterDictionary)
            {
                entry.RefreshDictionaryKey((int) id);
            }

            OnDataAdded.Invoke();

#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif

            return (int) id;
        }

        #region EDITOR UTILS

        private bool IsMasterDic()
        {
            return MasterKeys != null;
        }

        private ValueDropdownList<int> MasterDicDrop()
        {
            if (MasterKeys == null)
            {
                return new ValueDropdownList<int>();
            }

            return MasterKeys.ContentIDs;
        }


        [InfoBox("Can't add the entry, the ID already exists",
            InfoMessageType.Warning, "IsIDUnAvailable")]
        [TabGroup("Tools/Tabs", "Add Entry"), ShowIf("IsMasterDic"), ValueDropdown("MasterDicDrop")]
        private int idToAdd = -1;

        private bool IsIDUnAvailable()
        {
            return IsMasterDic() && DataByID.ContainsKey(idToAdd);
        }

        [TabGroup("Tools/Tabs", "Add Entry"), HideIf("IsIDUnAvailable")] [ShowInInspector]
        private List<T2> entriesToAdd;

        [TabGroup("Tools/Tabs", "Add Entry"), HideIf("IsIDUnAvailable"), Button(ButtonSizes.Medium)]
        private void AddEntries()
        {
            foreach (var entry in entriesToAdd)
            {
                AddEntry(entry);
            }
        }

        [TabGroup("Tools/Tabs", "Add Entry"), HideIf("IsIDUnAvailable"), Button(ButtonSizes.Medium),
         PropertyOrder(Int32.MaxValue)]
        private void AddEntry(T2 entryToAdd)
        {
            if (MasterKeys != null)
            {
                if (DataByID.ContainsKey(idToAdd))
                {
                    if (FirstAvailable != null)
                    {
                        idToAdd = (int) FirstAvailable;
                    }

                    return;
                }

                DataByID.Add(idToAdd, entryToAdd);
            }
            else
            {
                AddDataHolder(entryToAdd);
            }

            if (FirstAvailable != null)
            {
                idToAdd = (int) FirstAvailable;
            }

            if (!(entryToAdd is UnityEngine.Object))
                entryToAdd = CreateNewDataHolder(null);
        }

        [InfoBox("The Id is not present in the dictionary", InfoMessageType.Warning, "IsEditionKeyUnAvailable")]
        [TabGroup("Tools/Tabs", "Set Entry"), ValueDropdown("MasterDicDrop"), ShowIf("IsMasterDic")]
        private int editionID = -1;

        private bool IsEditionKeyUnAvailable()
        {
            return !DataByID.ContainsKey(editionID);
        }

        [TabGroup("Tools/Tabs", "Set Entry"), HideIf("IsEditionKeyUnAvailable")]
        private T2 EditedValue
        {
            get
            {
                if (DataByID.ContainsKey(editionID))
                {
                    return DataByID[editionID];
                }

                return default(T2);
            }
            set
            {
                if (DataByID.ContainsKey(editionID))
                {
                    DataByID[editionID] = value;
                }
            }
        }


        public ValueDropdownList<int> ContentIDs
        {
            get
            {
                ValueDropdownList<int> list = new ValueDropdownList<int>();

                foreach (var dataEntry in DataByID)
                {
                    if (dataEntry.Value == null || dataEntry.Value.Data == null)
                    {
                        continue;
                    }

                    if (dataEntry.Value != null)
                        list.Add(
                            dataEntry.Value.NameID.IsNullOrEmpty()
                                ? dataEntry.Value.Data.ToString()
                                : dataEntry.Value.NameID, dataEntry.Key);
                }

                return list;
            }
        }

        public IEnumerable<T> Values
        {
            get { return DataByID.Values.Convert(_ => _.Data); }
        }

        public Dictionary<int, T2> InternalDictionary => DataByID;

        [Button(ButtonSizes.Medium), FoldoutGroup("Tools")]
        public void RemoveNullElements()
        {
            if (DataByID == null)
                return;

            foreach (var key in DataByID.Keys.ToArray())
            {
                if (DataByID[key] == null || DataByID[key].Data == null)
                {
                    DataByID.Remove(key);
                }
            }

#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        [Button(ButtonSizes.Medium), FoldoutGroup("Tools")]
        public void RemoveDuplicateElements()
        {
            if (DataByID == null)
                return;

            List<T2> foundData = new List<T2>();

            foreach (var key in DataByID.Keys.ToArray())
            {
                if (foundData.Contains(DataByID[key]))
                {
                    DataByID.Remove(key);
                }
                else
                {
                    foundData.Add(DataByID[key]);
                }
            }

#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        #endregion

        #region SPREADSHEET PARSING

        public virtual void ParseTableData(string jsonTableData, bool createMissingData = true,
            bool deleteMissingData = false)
        {
            if (DataByID == null)
                DataByID = new Dictionary<int, T2>();

            RemoveNullElements();
            RemoveDuplicateElements();

            jsonTableData = jsonTableData.TrimStart('[').TrimEnd('\n').TrimEnd(']');
            Dictionary<int, JsonData> jSonDataByIds = new Dictionary<int, JsonData>();
            string[] rowSplit = jsonTableData.Split(new[] {"},"}, StringSplitOptions.None);

            foreach (var s in rowSplit)
            {
                JsonData data = new JsonData();
                string jSonString = s;
                if (!jSonString.EndsWith("}"))
                {
                    jSonString = jSonString + "}";
                }

                if (LogDebug)
                {
                    UnityEngine.Debug.LogFormat(this, "Parsing json {0} on dictionary {1}", jSonString, name);
                }

                data.PopulateData(jSonString);

                if (!data.IsValid)
                {
                    if (LogDebug)
                    {
                        UnityEngine.Debug.LogWarningFormat(this, "Json {0} on dictionary {1} could not parsed properly", jSonString,
                            name);
                    }

                    continue;
                }

                if (jSonDataByIds.ContainsKey(data.ID))
                {
                    UnityEngine.Debug.LogWarning("Id duplication (" + data.ID + ") during the json reading: one ID will be ignored");
                    continue;
                }


                if (LogDebug)
                {
                    UnityEngine.Debug.LogFormat(this, "Adding Json Data for ID {0}", data.ID);
                }

                jSonDataByIds.Add(data.ID, data);
            }

            foreach (var jsonData in jSonDataByIds)
            {
                if (DataByID.ContainsKey(jsonData.Key) && DataByID[jsonData.Key] != null &&
                    DataByID[jsonData.Key].Data != null)
                {
                    if (LogDebug)
                    {
                        UnityEngine.Debug.LogFormat(this, "Setting Json Data Values for ID {0}", jsonData.Key);
                    }

                    DataByID[jsonData.Key].ParseJsonData(jsonData.Value);
                }
                else if (createMissingData)
                {
                    DataByID.Remove(jsonData.Key);
                    T2 newData = CreateNewDataHolder(jsonData.Value);
                    if (newData == null)
                    {
                        if (LogDebug)
                        {
                            UnityEngine.Debug.LogWarning("Data Holder Creation Failed For ID" + jsonData.Key, this);
                        }

                        continue;
                    }

                    newData.ParseJsonData(jsonData.Value);
                    if (LogDebug)
                    {
                        UnityEngine.Debug.Log(newData + "for ID " + jsonData.Key + " from Json " + jsonData);
                    }

                    DataByID.AddOrReplace(jsonData.Key, newData);
                }
            }

            if (deleteMissingData)
            {
                List<int> idToRemove = new List<int>();
                foreach (var data in DataByID)
                {
                    if (!jSonDataByIds.ContainsKey(data.Key))
                    {
                        data.Value.NotifyJSonDataDeleteRequest();
                        idToRemove.Add(data.Key);
                        if (LogDebug)
                        {
                            UnityEngine.Debug.Log("Deleting data with ID " + data.Key);
                        }
                    }
                }

                DataByID.RemoveAll(idToRemove);
            }

#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        public virtual string GetTableDataAsJson()
        {
            StringBuilder json = new StringBuilder();
            json.Append("[");
            bool start = true;

            foreach (var data in DataByID)
            {
                if (!start)
                {
                    json.Append(",");
                }

                JsonData jdata = data.Value.ExportJsonData();
                jdata.ID = data.Key;
                json.Append(jdata.ExportJson());

                start = false;
            }

            json.Append("]");
            return json.ToString();
        }

        public Dictionary<int, JsonData> GetTableDataSeparatedByRow()
        {
            Dictionary<int, JsonData> datas = new Dictionary<int, JsonData>();

            foreach (var data in DataByID)
            {
                if (data.Value == null)
                {
                    continue;
                }

                JsonData jdata = data.Value.ExportJsonData();
                jdata.ID = data.Key;
                datas.Add(data.Key, jdata);
            }

            return datas;
        }

        #endregion
    }
}
