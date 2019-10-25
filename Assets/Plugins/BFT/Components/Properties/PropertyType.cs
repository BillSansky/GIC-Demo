using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace BFT
{
    public class PropertyType : SerializedScriptableObject
    {
#if UNITY_EDITOR

        [Multiline] public string Description;

        [FormerlySerializedAs("ExpectsType")] public bool ExpectsDataType;

        [FormerlySerializedAs("TypeExpected")] [ShowIf("ExpectsDataType"), ValueDropdown("GetTypes")]
        public Type DataTypeExpected;

        private ValueDropdownList<Type> GetTypes()
        {
            var q = typeof(PropertyComponent).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => !x.IsGenericTypeDefinition)
                .Where(x => typeof(PropertyComponent).IsAssignableFrom(x));
            var valueDD = new ValueDropdownList<Type>();
            q.ForEach(_ => valueDD.Add(_.Name, _));
            return valueDD;
        }
#endif
    }
}
