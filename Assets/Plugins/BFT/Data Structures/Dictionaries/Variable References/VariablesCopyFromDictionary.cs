using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    /// <summary>
    ///     A useful class when you want to link all at once few elements from a dictionary, for instance when working on
    ///     generic UI elements
    ///     Given a dictionary, will associate values to all the specified variables by id references.
    /// </summary>
    /// <typeparam name="T"> the type of data in the dictionary</typeparam>
    /// <typeparam name="T1"> the reference value type to the dictionary</typeparam>
    /// <typeparam name="T2"> the type of entry in the dictionary</typeparam>
    /// <typeparam name="T3">the type of dictionary</typeparam>
    /// <typeparam name="T4">the type of entry dictionary that will contain the variables</typeparam>
    /// <typeparam name="T5">the type of variable that will be used for references</typeparam>
    /// <typeparam name="T6">the type of entry that will be used for teh variable to use</typeparam>
    public class VariablesCopyFromDictionary<T, T1, T2, T3, T4, T5, T6> : MonoBehaviour
        where T1 : EntryDictionaryValue<T, T2, T3>
        where T2 : class, IDictionaryEntry<T>
        where T3 : EntryDictionary<T, T2>
        where T4 : EntryDictionary<T5, T6>
        where T5 : class, IVariable<T>
        where T6 : class, IDictionaryEntry<T5>
    {
        [BoxGroup("Content")] public T4 Content;

        [BoxGroup("Reference"), PreviouslySerializedAs("CurrentContentProvider")]
        public T1 CurrentDictionary;

        [BoxGroup("Events")] public UnityEvent OnContentAssigned;

        private ValueDropdownList<int> IDDropdown()
        {
            if (CurrentDictionary != null)
                return CurrentDictionary.Value.ContentIDs;
            return new ValueDropdownList<int>();
        }

        [FoldoutGroup("Tools"), Button(ButtonSizes.Medium), PropertyOrder(9999)]
        public void AssignContent()
        {
            foreach (var key in Content)
            {
                if (CurrentDictionary == null)
                    continue;

                try
                {
                    if (CurrentDictionary.Value.ContainsKey(key))
                        Content[key].Value = CurrentDictionary.Value.Get(key);
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogError("Exception in Dictionary: " + e.Message, this);
                }
            }

            OnContentAssigned.Invoke();
        }

        public void SwitchContentProvider(IReadOnlyDictionary<T> newProvider, bool assign = true)
        {
            // CurrentDictionary = newProvider;
            if (assign)
                AssignContent();
        }

        #region EDITOR TOOLS

        [FoldoutGroup("Tools/Adding Content")] private T5 toAdd;

        [FoldoutGroup("Tools/Adding Content")]
        [Button(ButtonSizes.Medium)]
        public void AddContent()
        {
            Content.Add(toAdd);
        }

        #endregion
    }
}
