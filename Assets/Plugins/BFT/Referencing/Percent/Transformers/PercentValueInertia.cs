using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    [AddComponentMenu("Referencing/Percent/Percent Inertia")]
    public class PercentValueInertia : SerializedMonoBehaviour, IValue<float>
    {
        private float currentValue;
        public IValue<float> PercentReference;
        public float SpeedDown;

        public float SpeedUp;

        public float Value => Mathf.Clamp01(currentValue);

        void Start()
        {
            currentValue = PercentReference.Value;
        }

        void Update()
        {
            currentValue = Mathf.Lerp(currentValue, PercentReference.Value,
                (PercentReference.Value > currentValue) ? SpeedUp * UnityEngine.Time.deltaTime : SpeedDown * UnityEngine.Time.deltaTime);
        }
    }
}
