using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class EnumToObject : SerializedMonoBehaviour, IValue<UnityEngine.Object>
    {
        [OnValueChanged("FillDictionary")] public IValue<System.Enum> EnumReference;

        public Dictionary<System.Enum, UnityEngine.Object> ObjectPerEnumID;

        [ShowInInspector, BoxGroup("Status"), ReadOnly]
        public UnityEngine.Object Value
        {
            get
            {
#if UNITY_EDITOR
                if (!Application.isPlaying && EnumReference == null || !ObjectPerEnumID.ContainsKey(EnumReference.Value))
                {
                    return null;
                }
#endif
                return ObjectPerEnumID[EnumReference.Value];
            }
        }

        private void FillDictionary()
        {
            ObjectPerEnumID.Clear();
            foreach (var enumValue in System.Enum.GetValues(EnumReference.Value.GetType()))
            {
                ObjectPerEnumID.Add((System.Enum) enumValue, null);
            }
        }
    }
}
