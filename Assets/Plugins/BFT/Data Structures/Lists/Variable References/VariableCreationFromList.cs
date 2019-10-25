using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    /// <summary>
    ///     Creates variable references from list elements
    ///     useful for generic UIs that may contain a certain amount of objects to link:
    ///     this way you can base part of the UI on a reference, and ignore where this reference comes from.
    ///     The amount of types is a mouthful unfortunately, but that's the only to have the data properly serialized
    /// </summary>
    /// <typeparam name="T"> the type of variable</typeparam>
    /// <typeparam name="T1">the type of list value</typeparam>
    /// <typeparam name="T2">the type of variable that will be used to copy the data</typeparam>
    /// <typeparam name="T3">the type of unity event that will be used by the variable in the prefab</typeparam>
    public class VariableCreationFromList<T, T1, T2> : MonoBehaviour
        where T1 : IValue<List<T>>
        where T2 : VariableComponent<T>
    {
        [BoxGroup("Status"), ReadOnly, HideInEditorMode]
        private readonly List<T2> currentComponents = new List<T2>();

        private readonly List<T2> notAssignedComponents
            = new List<T2>();

        [BoxGroup("Options")] public bool AssignOnEnable = true;

        [InfoBox("The Component that will be instantiated and that will hold the value of each list entry")]
        [BoxGroup("References")]
        public T2 ComponentPrefab;

        [BoxGroup("Options"), ShowIf("PoolComponentsOnAwake")]
        public int InitialPoolSize = 5;

        [BoxGroup("References")] public T1 List;

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
            foreach (var variableComponent in currentComponents)
            {
                SetComponentNonAssigned(variableComponent);
            }
        }

        [BoxGroup("Tools")]
        [Button(ButtonSizes.Medium)]
        public void ReAssignAllComponents()
        {
            if (List.Value.Count < currentComponents.Count)
                PoolComponents(currentComponents.Count - List.Value.Count);

            for (var index = 0; index < List.Value.Count; index++)
            {
                var listValue = List.Value[index];
                if (listValue == null)
                    continue;

                if (currentComponents.Count < List.Value.Count)
                {
                    PullNewComponent();
                    AssignComponent(index, listValue);
                }
                else
                {
                    AssignComponent(index, listValue);
                }
            }
        }

        private void SetComponentNonAssigned(T2 component)
        {
            component.Value = default(T);
            component.gameObject.SetActive(false);
            currentComponents.Remove(component);
            notAssignedComponents.Add(component);
        }

        [BoxGroup("Tools")]
        [Button(ButtonSizes.Medium)]
        public void AssignMissingValues()
        {
            if (List.Value.Count < currentComponents.Count)
                PoolComponents(currentComponents.Count - List.Value.Count);

            for (var index = 0; index < List.Value.Count; index++)
            {
                var listValue = List.Value[index];
                if (listValue == null)
                    continue;

                if (currentComponents.Count < List.Value.Count)
                {
                    PullNewComponent();
                    AssignComponent(index, listValue);
                }
                else if (currentComponents[index].Value == null || !currentComponents[index].Value.Equals(listValue))
                {
                    AssignComponent(index, listValue);
                }
            }
        }

        private void PullNewComponent()
        {
            if (notAssignedComponents.Count > 0)
            {
                currentComponents.Add(notAssignedComponents[0]);
                notAssignedComponents.RemoveAt(0);
            }
            else
            {
                if (LogPoolExpanding)
                    UnityEngine.Debug.LogFormat(this, "Pool Expending with {0} new elements", 1);

                T2 newComponent = Instantiate(ComponentPrefab, transform);
                currentComponents.Add(newComponent);
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
