using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    
    /// <summary>
    ///     a dictionary that has an int id and a way to sort its values using name IDs for an easier user experience
    /// </summary>
    /// <typeparam name="T">the type of data handled in the dictionary</typeparam>
    /// <typeparam name="T2">the type that hold the data and will be use as the value for each key.</typeparam>
    public abstract class EntryDictionary<T, T2> : SerializableDictionaryBase<int, T2>, IEntryDictionary<T>
        where T2 : class, IDictionaryEntry<T>
    {
        [FoldoutGroup("Master Dictionary", false, 90)]
        [Tooltip(
            "If the dictionary is a master dictionary, it will refresh the keys of its member when the get added to the dictionary")]
        public bool IsMasterDictionary = true;

        [SerializeField, FoldoutGroup("Master Dictionary", false, 90)]
        public IIDsHolder MasterKeys;

        [FoldoutGroup("Events", false)] public UnityEvent OnDataAdded;

        [SerializeField, FoldoutGroup("Master Dictionary")]
        public IEntryDictionary<T> ParentEntryDictionary;

        private int? FirstAvailable =>
            Enumerable.Range(0, int.MaxValue).Except(Keys).FirstOrDefault();

        public IEnumerable<int> Keys => Keys;

        public bool ContainsKey(int id)
        {
            if (ParentEntryDictionary == null)
            {
                return _dict.ContainsKey(id);
            }

            return _dict.ContainsKey(id) || ParentEntryDictionary.ContainsKey(id);
        }

        public virtual T Get(int id)
        {
            if (_dict.ContainsKey(id))
            {
                UnityEngine.Debug.Assert(_dict[id] != null, "The data you are trying to access is null");

                return _dict[id].Data;
            }

            if (ParentEntryDictionary != null)
            {
                return ParentEntryDictionary.Get(id);
            }

            UnityEngine.Debug.LogWarning("The key was not found in the dictionary, returning default value");

            return default(T);
        }

        public T this[int index]
        {
            get => Get(index);
            set => Set(index, value);
        }

        public virtual void Set(int id, T value)
        {
            if (_dict.ContainsKey(id))
            {
                _dict[id].Data = value;
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

            return -1;
        }

        public void Remove(int id)
        {
            _dict.Remove(id);
        }

        public IEnumerator<int> GetEnumerator()
        {
            foreach (var value in _dict.Keys)
            {
                yield return value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [FoldoutGroup("Tools", false, 99999), Button(ButtonSizes.Medium), ShowIf("IsMasterDictionary")]
        private void RefreshAllEntries()
        {
            if (!IsMasterDictionary)
            {
                return;
            }

            foreach (var id in this)
            {
                var value = _dict[id];
                if (value.ID == id)
                {
                    continue;
                }

                value.RefreshDictionaryKey(id);
            }
        }

        public T2 GetDataByName(string name)
        {
            return _dict.FirstOrDefault(_ => _.Value.NameID == name).Value;
        }

        public abstract T2 CreateNewDataHolder(object input);

        /// <summary>
        ///     warning, costly call
        /// </summary>
        /// <param name="data"></param>
        public int GetFirstID(T value)
        {
            foreach (var data in _dict)
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
            foreach (var data in _dict)
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
            foreach (var data in _dict)
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
            _dict.Clear();
        }

        public bool GetNotNullValue(out T2 data)
        {
            if (_dict.Values.Any(_ => !_.IsNull()))
            {
                data = _dict.Values.First(_ => !_.IsNull());
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
            foreach (var kvp in _dict)
            {
                if (kvp.Value == entry)
                {
                    _dict.Remove(kvp.Key);
                    break;
                }
            }
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

            _dict.Add((int) id, entry);

            if (IsMasterDictionary)
            {
                entry.RefreshDictionaryKey((int) id);
            }

            OnDataAdded.Invoke();

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
            return IsMasterDic() && _dict.ContainsKey(idToAdd);
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
                if (_dict.ContainsKey(idToAdd))
                {
                    if (FirstAvailable != null)
                    {
                        idToAdd = (int) FirstAvailable;
                    }

                    return;
                }

                _dict.Add(idToAdd, entryToAdd);
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
            return !_dict.ContainsKey(editionID);
        }

        [TabGroup("Tools/Tabs", "Set Entry"), HideIf("IsEditionKeyUnAvailable")]
        private T2 EditedValue
        {
            get
            {
                if (_dict.ContainsKey(editionID))
                {
                    return _dict[editionID];
                }

                return default(T2);
            }
            set
            {
                if (_dict.ContainsKey(editionID))
                {
                    _dict[editionID] = value;
                }
            }
        }


        public ValueDropdownList<int> ContentIDs
        {
            get
            {
                ValueDropdownList<int> list = new ValueDropdownList<int>();

                foreach (var dataEntry in _dict)
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
            get { return _dict.Values.Convert(_ => _.Data); }
        }

        public Dictionary<int, T2> InternalDictionary => _dict;

        [Button(ButtonSizes.Medium), FoldoutGroup("Tools")]
        public void RemoveNullElements()
        {
            if (_dict == null)
                return;

            foreach (var key in _dict.Keys.ToArray())
            {
                if (_dict[key] == null || _dict[key].Data == null)
                {
                    _dict.Remove(key);
                }
            }
        }

        [Button(ButtonSizes.Medium), FoldoutGroup("Tools")]
        public void RemoveDuplicateElements()
        {
            List<T2> foundData = new List<T2>();

            foreach (var key in _dict.Keys.ToArray())
            {
                if (foundData.Contains(_dict[key]))
                {
                    _dict.Remove(key);
                }
                else
                {
                    foundData.Add(_dict[key]);
                }
            }
        }

        #endregion
    }
}
