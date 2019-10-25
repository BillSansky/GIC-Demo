using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class FloatDivisionValueComponent : MonoBehaviour, IValue<float>
    {
        public FloatValue DividerValue = new FloatValue() {LocalValue = 1};
        public FloatValue FloatValue;

        [ShowInInspector]
        public float Value
        {
            get
            {
                if (Math.Abs(DividerValue.Value) < 0.00000001f)
                {
                    UnityEngine.Debug.LogWarning("Attempting to divide by zero!", this);
                    return 0;
                }

                return FloatValue.Value / DividerValue.Value;
            }
        }
    }
}
