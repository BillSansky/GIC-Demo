using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace BFT
{
    /// <summary>
    ///     This is a self referenced dictionary,
    ///     which means that the objects are their own data containers.
    ///     If you would want the scriptable objects to not be self contained, you will need to create your own class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ScriptableObjectDictionaryAsset<T> : DictionaryAsset<T, T>
        where T : ScriptableObject, IDictionaryEntry<T>
    {
        [FoldoutGroup("Json Database Options", false, 999)]
        public bool CreateNewElementInSameDirectory = true;

        public override T CreateNewDataHolder(object data)
        {
            JsonData jData = (JsonData) data;
            return CreateNewDataHolder("", jData.DataByID.ContainsKey("TypeID") ? jData["TypeID"] : "");
        }


        public override void Set(int id, T value)
        {
            if (LogDebug)
            {
                UnityEngine.Debug.LogFormat(this, "Dictionary {2} setting data {1} for id {0}", id, value, name);
            }

            if (InternalDictionary.ContainsKey(id))
            {
                InternalDictionary[id] = value;
            }
            else
            {
                AddData(value, id);
            }

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        public virtual T CreateInstanceOfType(string type)
        {
            return CreateInstance<T>();
        }

        public virtual T CreateNewDataHolder(string name, string type)
        {
            //the type is ignored by default
            if (LogDebug)
            {
                UnityEngine.Debug.LogFormat(this, "Creating a new Data holder for Dictionary {0}", name);
            }

            if (name.ContainsAny(new char[] {'?', '\\', '*', '/', '|', '<', '>'}))
            {
                UnityEngine.Debug.LogWarning(
                    "Cannot create a new asset named \"" + name + "\" because it contains forbidden characters");
                return null;
            }

            T instance = CreateInstanceOfType(type);
            instance.name = name;
#if UNITY_EDITOR
            string folder;
            if (!CreateNewElementInSameDirectory && InternalDictionary.Count > 0)
            {
                folder = Path.GetDirectoryName(
                    UnityEditor.AssetDatabase.GetAssetPath(InternalDictionary.Values.ElementAt(0)));
            }
            else
            {
                folder = Path.GetDirectoryName(UnityEditor.AssetDatabase.GetAssetPath(this));
            }

            if (instance.name.IsNullOrEmpty())
            {
                instance.name = "New Instance";
                while (File.Exists(folder + "/" + instance.name + ".asset"))
                {
                    instance.name += "(copy)";
                    if (LogDebug)
                    {
                        UnityEngine.Debug.LogFormat(this,
                            "Renaming Asset Because a file with the same name already exists");
                    }
                }
            }

            if (LogDebug)
            {
                UnityEngine.Debug.LogFormat(this, "Creating Asset named {0} in folder {1} for dictionary {2}",
                    instance.name, folder, name);
            }

            UnityEditor.AssetDatabase.CreateAsset(instance, folder + "/" + instance.name + ".asset");
#endif
            return instance;
        }


        public static void AddAllSelectedDictionaryMembers()
        {
#if UNITY_EDITOR
            ScriptableObjectDictionaryAsset<T> dictionary =
                (ScriptableObjectDictionaryAsset<T>)
                UnityEditor.Selection.objects.First(_ => _ is ScriptableObjectDictionaryAsset<T>);

            if (dictionary == null)
            {
                UnityEngine.Debug.LogWarning("Select a dictionary for this command to work");
                return;
            }

            IEnumerable<T> elements = UnityEditor.Selection.objects.Where(_ => _ is T).Convert(_ => _ as T);

            foreach (var element in elements)
            {
                if (!dictionary.ContainsValueHolder(element))
                {
                    element.RefreshDictionaryKey(dictionary.AddDataHolder(element));
                }
            }
#endif
        }

        [FoldoutGroup("Tools", false, 99999)]
        [Button(ButtonSizes.Medium)]
        public void FindAndAddAllObjectOfType()
        {
            string[] assetsPaths = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
            foreach (var path in assetsPaths)
            {
                var obj = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(path));
                if (!DataByID.ContainsValue(obj))
                {
                    AddDataHolder(obj);
                }
            }
        }
    }
}
