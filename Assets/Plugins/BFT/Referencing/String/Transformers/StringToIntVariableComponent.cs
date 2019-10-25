using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class StringToIntVariableComponent : SerializedMonoBehaviour, IValue<int>
    {
        [SerializeField] private int defaultValue;

        [BoxGroup("Reference")] public StringValue StringValue;

        public int Value
        {
            get
            {
                int parsedValue;
                if (int.TryParse(StringValue.Value, out parsedValue))
                    return parsedValue;
                return defaultValue;
            }
        }
    }
}
