using System;
using UnityEngine;

namespace BFT
{
    [Serializable]
    public class PropertyComponentDataSetter
    {
        public PropertyType PropertyToSet;

        public object GetPropertyFromGameObject(GameObject go)
        {
            var group = go.GetComponent<PropertyComponentGroup>();

            if (group)
            {
                if (group.HasCategory(PropertyToSet))
                {
                    return group.GetPropertyComponentForCategory(PropertyToSet).Data;
                }

                return null;
            }

            var components = go.GetComponents<PropertyComponent>();

            if (components.Length > 0)
            {
                foreach (var propertyComponent in components)
                {
                    if (PropertyToSet == propertyComponent.PropertyType)
                    {
                        return propertyComponent.Data;
                    }
                }


                return null;
            }

            UnityEngine.Debug.LogWarning(
                $"No Property component or property component group was found on object {go}, this is invalid", go);

            return null;
        }
    }
}
