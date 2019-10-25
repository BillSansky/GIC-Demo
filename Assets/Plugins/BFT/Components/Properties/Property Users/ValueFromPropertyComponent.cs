using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public abstract class ValueFromPropertyComponent<T, T1> : MonoBehaviour, IValue<T> where T1 : GenericValue<T>, new()
    {
        [Unity.Collections.ReadOnly, ShowInInspector, BoxGroup("Status")]
        private PropertyComponent currentProperty;

        public T1 DefaultValue;

        public T Value => currentProperty ? ((IValue<T>) currentProperty.Data).Value : DefaultValue.Value;

        public void AssignProperty(PropertyComponent component)
        {
            currentProperty = component;
        }
    }
}
