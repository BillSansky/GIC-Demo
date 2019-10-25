using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class FloatToIntValueComponent : MonoBehaviour, IValue<int>
    {
        [BoxGroup("Reference")] public FloatValue FloatValue;

        [SerializeField, BoxGroup("Options")] private Rounding RoundMethod = Rounding.Floor;

        public int Value
        {
            get
            {
                switch (RoundMethod)
                {
                    case Rounding.Floor:
                        return Mathf.FloorToInt(FloatValue.Value);
                    case Rounding.Ceil:
                        return Mathf.CeilToInt(FloatValue.Value);
                    case Rounding.Near:
                        return Mathf.RoundToInt(FloatValue.Value);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        enum Rounding
        {
            Floor,
            Ceil,
            Near
        }
    }
}
