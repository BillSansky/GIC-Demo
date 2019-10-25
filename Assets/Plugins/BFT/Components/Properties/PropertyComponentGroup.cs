using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace BFT
{
    public class PropertyComponentGroup : MonoBehaviour
    {
        [SerializeField] [FormerlySerializedAs("Components"), OnValueChanged("CheckForDuplicateCategories")]
        private List<PropertyComponent> PropertyComponents = new List<PropertyComponent>();

        private Dictionary<PropertyType, PropertyComponent> PropertyComponentsPerCategory;

        public void Awake()
        {
            PropertyComponentsPerCategory =
                new Dictionary<PropertyType, PropertyComponent>(PropertyComponents.Count);
            PropertyComponents.ForEach(_ => PropertyComponentsPerCategory.Add(_.PropertyType, _));
        }

        private void CheckForDuplicateCategories()
        {
            foreach (var prop in new List<PropertyComponent>(PropertyComponents))
            {
                if (PropertyComponents.Any(_ => _ != prop && prop.PropertyType == _.PropertyType))
                {
                    PropertyComponents.Remove(prop);
                    Debug.LogWarning(
                        $"You cannot have few properties in the same category ( {prop.name} is problematic )",
                        this);
                }
            }
        }

        public PropertyComponent GetPropertyComponentForCategory(PropertyType category)
        {
            Debug.Assert(PropertyComponentsPerCategory.ContainsKey(category),
                $"The required category ({category}) was not defined this his component group", this);
            return PropertyComponentsPerCategory[category];
        }

        public bool HasCategory(PropertyType category)
        {
            return PropertyComponentsPerCategory.ContainsKey(category);
        }

        [Button(ButtonSizes.Medium)]
        public void AddAllPropertiesInChildren()
        {
            foreach (var propertyComponent in gameObject.GetComponentsInChildren<PropertyComponent>())
            {
                if (!PropertyComponents.Contains(propertyComponent))
                {
                    if (PropertyComponents.Any(_ => _.PropertyType == propertyComponent.PropertyType))
                    {
                        Debug.LogWarning(
                            $"You cannot have few properties in the same category ( {propertyComponent.name} is problematic )",
                            this);
                        continue;
                    }

                    PropertyComponents.Add(propertyComponent);
                }
            }
        }
    }
}
