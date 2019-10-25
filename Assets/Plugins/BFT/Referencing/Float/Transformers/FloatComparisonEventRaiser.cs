using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    public class FloatComparisonEventRaiser : MonoBehaviour
    {
        [SerializeField] private FloatValue firstValue;

        [SerializeField, BoxGroup("Events")] private UnityEvent OnEqual;

        [SerializeField, BoxGroup("Events")] private UnityEvent OnFirstBigger;

        [SerializeField, BoxGroup("Events")] private UnityEvent OnSecondBigger;

        [SerializeField] private FloatValue secondValue;

        [SerializeField] private float tolerance = 0.001f;

        [Button(ButtonSizes.Medium)]
        public void Compare()
        {
            if (Mathf.Abs(firstValue.Value - secondValue.Value) < tolerance)
                OnEqual.Invoke();
            else if (firstValue.Value > secondValue.Value)
                OnFirstBigger.Invoke();
            else if (firstValue.Value < secondValue.Value)
                OnSecondBigger.Invoke();
        }
    }
}
