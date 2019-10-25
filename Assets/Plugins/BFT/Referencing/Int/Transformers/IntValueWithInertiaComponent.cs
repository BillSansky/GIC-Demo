using UnityEngine;

namespace BFT
{
    public class IntValueWithInertiaComponent : MonoBehaviour,
        IValue<int>
    {
        private int currentValue;
        public IntValue IntValue;
        public int SpeedDown;

        public int SpeedUp;

        public int Value => IntValue.Value;

        void Start()
        {
            currentValue = IntValue.Value;
        }

        void Update()
        {
            currentValue = (int) Mathf.Lerp(currentValue, IntValue.Value,
                (IntValue.Value > currentValue) ? SpeedUp * UnityEngine.Time.deltaTime : SpeedDown * UnityEngine.Time.deltaTime);
        }
    }
}
