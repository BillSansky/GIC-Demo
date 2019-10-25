using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

#endif


namespace BFT
{
    public class FloatValueInertiaComponent : SerializedMonoBehaviour,
        IValue<float>
    {
        [ShowInInspector, ReadOnly] private float currentValue;
        public FloatValue FloatGiver;
        public FloatValue SpeedDown;

        public FloatValue SpeedUp;

        public float FloatValue => currentValue;

        public float Value => FloatValue;

        void Start()
        {
            currentValue = FloatGiver.Value;
        }

        void Update()
        {
            currentValue = Mathf.Lerp(currentValue, FloatGiver.Value,
                (FloatGiver.Value > currentValue) ? SpeedUp.Value * UnityEngine.Time.deltaTime : SpeedDown.Value * UnityEngine.Time.deltaTime);
        }

        public void ForceTargetValue()
        {
            currentValue = FloatGiver.Value;
        }

        public void OnDrawGizmosSelected()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                currentValue = Mathf.Lerp(currentValue, FloatGiver.Value,
                    (FloatGiver.Value > currentValue) ? SpeedUp.Value * 0.02f : SpeedDown.Value * 0.02f);
                EditorUtility.SetDirty(this);
                return;
            }
#endif
        }
    }
}
