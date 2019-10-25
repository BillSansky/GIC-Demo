using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class MultiTypeFloatValue : MonoBehaviour, IValue<float>
    {
        public enum FloatVariableType
        {
            RANDOM,
            SMOOTH_RANDOM,
            COMPONENT,
            TIMER_PERCENT,
        }

        [ShowIf("Random")] public FloatRandomValueComponent Random;

        [ShowIf("VariableType", FloatVariableType.SMOOTH_RANDOM)]
        public FloatSmoothRandomValueComponent SmoothRandom;

        [ShowIf("VariableType", FloatVariableType.TIMER_PERCENT)]
        public Timer Timer;

        [ShowIf("IsComponent")] public FloatVariableComponent VariableComponent;

        public FloatVariableType VariableType;

        private bool IsRandom => VariableType == FloatVariableType.RANDOM;
        private bool IsComponent => VariableType == FloatVariableType.COMPONENT;

        public float Value
        {
            get
            {
                switch (VariableType)
                {
                    case FloatVariableType.RANDOM:
                        return Random.Value;
                    case FloatVariableType.SMOOTH_RANDOM:
                        return SmoothRandom.Value;
                    case FloatVariableType.COMPONENT:
                        return VariableComponent.Value;
                    case FloatVariableType.TIMER_PERCENT:
                        return Timer.Percent;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
