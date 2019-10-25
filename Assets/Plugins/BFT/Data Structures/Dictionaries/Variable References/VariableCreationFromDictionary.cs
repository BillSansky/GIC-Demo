using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    /// <summary>
    ///     Creates variable references from dictionary elements
    ///     useful for generic UIs that may contain a certain amount of objects to link:
    ///     this way you can base part of the UI on a reference, and ignore where this reference comes from.
    ///     The amount of types is a mouthful unfortunately, but that's the only to have the data properly serialized
    /// </summary>
    /// <typeparam name="T"> the type of variable</typeparam>
    /// <typeparam name="T1">the type of entry dictionary</typeparam>
    /// <typeparam name="T2">the type of variable that will be used to copy the data</typeparam>
    /// <typeparam name="T3">the type of unity event that will be used by the variable in the prefab</typeparam>
    public class VariableCreationFromDictionary<T, T1, T2> : MonoBehaviour
        where T1 : IValue<IEntryDictionary<T>>
        where T2 : VariableComponent<T>
    {
        [BoxGroup("Status"), ReadOnly, HideInEditorMode]
        private readonly Dictionary<int, T2> currentComponents
            = new Dictionary<int, T2>();

        [BoxGroup("Status"), SerializeField] private readonly List<T2> notAssignedComponents
            = new List<T2>();

        [BoxGroup("Options")] public bool AssignOnEnable = true;

        [InfoBox("The Component that will be instantiated and that will hold the value of each dictionary entry")]
        [BoxGroup("References")]
        public T2 ComponentPrefab;

        [BoxGroup("References")] public T1 Dictionary;

        [BoxGroup("Options"), ShowIf("PoolComponentsOnAwake")]
        public int InitialPoolSize = 5;

        [BoxGroup("Options")] public bool LogPoolExpanding = false;

        [BoxGroup("Options")] public bool PoolComponentsOnAwake;

        public void OnEnable()
        {
            if (AssignOnEnable)
                ReAssignAllComponents();
        }

        [BoxGroup("Tools")]
        [Button(ButtonSizes.Medium)]
        public void ClearAllComponents()
        {
            foreach (var variableComponent in currentComponents.Keys.ToArray())
            {
                SetComponentNonAssigned(variableComponent);
            }
        }

        [BoxGroup("Tools")]
        [Button(ButtonSizes.Medium)]
        public void ReAssignAllComponents()
        {
            List<int> notReassigned
                = new List<int>(currentComponents.Keys);

            foreach (int i in Dictionary.Value)
            {
                T dictionaryValue = Dictionary.Value.Get(i);
                if (dictionaryValue == null)
                    continue;

                if (!currentComponents.ContainsKey(i))
                {
                    PullNewComponent(i);
                    AssignComponent(i, Dictionary.Value.Get(i));
                }
                else
                {
                    AssignComponent(i, dictionaryValue);
                    notReassigned.Remove(i);
                }
            }

            foreach (var component in notReassigned)
            {
                SetComponentNonAssigned(component);
            }
        }

        private void SetComponentNonAssigned(int id)
        {
            T2 component = currentComponents[id];
            component.Value = default(T);
            component.gameObject.SetActive(false);
            currentComponents.Remove(id);
            notAssignedComponents.Add(component);
        }

        [BoxGroup("Tools")]
        [Button(ButtonSizes.Medium)]
        public void AssignMissingValues()
        {
            foreach (int id in Dictionary.Value)
            {
                T dictionaryValue = Dictionary.Value.Get(id);
                if (dictionaryValue == null)
                    continue;

                if (!currentComponents.ContainsKey(id))
                {
                    PullNewComponent(id);
                    AssignComponent(id, dictionaryValue);
                }
                else if (currentComponents[id].Value == null || !currentComponents[id].Value.Equals(dictionaryValue))
                {
                    AssignComponent(id, dictionaryValue);
                }
            }
        }

        private void PullNewComponent(int id)
        {
            if (notAssignedComponents.Count > 0)
            {
                currentComponents.Add(id, notAssignedComponents[0]);
                notAssignedComponents.RemoveAt(0);
            }
            else
            {
                if (LogPoolExpanding)
                    UnityEngine.Debug.LogFormat(this, "Pool Expending with {0} new elements", 1);

                T2 newComponent = Instantiate(ComponentPrefab, transform);
                currentComponents.Add(id, newComponent);
            }
        }

        public void PoolComponents(int amount)
        {
            if (LogPoolExpanding)
                UnityEngine.Debug.LogFormat(this, "Pool Expending with {0} new elements", amount);

            for (int i = 0; i < amount; i++)
            {
                notAssignedComponents.Add(Instantiate(ComponentPrefab, transform));
            }
        }

        private void AssignComponent(int id, T content)
        {
            currentComponents[id].Value = content;
            currentComponents[id].gameObject.SetActive(true);
        }
    }
}
