using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    public class PropertyTypeChecker : MonoBehaviour
    {
        public List<PropertyType> CategoriesToCheck = new List<PropertyType>();

        public bool IssueWarningOnCategoryNotMet = true;

        [InfoBox("Returns the data of the category met")]
        public PropertyComponentEvent OnCategoryMet;

        public UnityEvent OnCategoryNotMet;

        public void CheckAnyCategoryOnGameObject(GameObject go)
        {
            var group = go.GetComponent<PropertyComponentGroup>();

            if (group)
            {
                foreach (var category in CategoriesToCheck)
                {
                    if (group.HasCategory(category))
                    {
                        OnCategoryMet.Invoke(group.GetPropertyComponentForCategory(category));
                        return;
                    }
                }

                OnCategoryNotMet.Invoke();
                if (IssueWarningOnCategoryNotMet)
                {
                    UnityEngine.Debug.LogWarning("The object does not have amy of the categories specified ", this);
                }

                return;
            }

            var components = go.GetComponents<PropertyComponent>();

            if (components.Length > 0)
            {
                foreach (var propertyComponent in components)
                {
                    if (CategoriesToCheck.Contains(propertyComponent.PropertyType))
                    {
                        OnCategoryMet.Invoke(propertyComponent);
                        return;
                    }
                }

                OnCategoryNotMet.Invoke();
                if (IssueWarningOnCategoryNotMet)
                {
                    UnityEngine.Debug.LogWarning("The object does not have amy of the categories specified ", this);
                }

                return;
            }

            UnityEngine.Debug.LogWarning(
                $"No Property component or property component group was found on object {go}, this is invalid",
                this);
        }
    }
}
