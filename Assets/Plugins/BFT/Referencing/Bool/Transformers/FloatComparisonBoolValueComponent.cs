using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class FloatComparisonBoolValueComponent : MonoBehaviour, IValue<bool>
    {
        [BoxGroup("Options")] public ComparisonOperations Condition = ComparisonOperations.LOWER;
        [BoxGroup("References")] public FloatValue First;
        [BoxGroup("References")] public FloatValue Second;

        [BoxGroup("Status"), ReadOnly, ShowInInspector]
        public bool Value
        {
            get
            {
#if UNITY_EDITOR
                if (!Application.isPlaying && (First == null || Second == null))
                    return false;
#endif

                switch (Condition)
                {
                    case ComparisonOperations.GREATER:
                        return First.Value > Second.Value;
                    case ComparisonOperations.GREATER_EQUAL:
                        return First.Value >= Second.Value;
                    case ComparisonOperations.LOWER:
                        return First.Value < Second.Value;
                    case ComparisonOperations.LOWER_EQUAL:
                        return First.Value <= Second.Value;
                    case ComparisonOperations.EQUAL:
                        return Mathf.Approximately(First.Value, Second.Value);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
