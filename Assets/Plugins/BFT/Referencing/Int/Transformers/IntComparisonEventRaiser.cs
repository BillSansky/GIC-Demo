using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    public class IntComparisonEventRaiser : MonoBehaviour
    {
        [SerializeField] private IntValue firstValue;

        [SerializeField, BoxGroup("Events")] private UnityEvent OnEqual;

        [SerializeField, BoxGroup("Events")] private UnityEvent OnFirstBigger;

        [SerializeField, BoxGroup("Events")] private UnityEvent OnSecondBigger;

        [SerializeField] private IntValue secondValue;

        [Button(ButtonSizes.Medium)]
        public void Compare()
        {
            if (firstValue.Value == secondValue.Value)
                OnEqual.Invoke();
            else if (firstValue.Value > secondValue.Value)
                OnFirstBigger.Invoke();
            else if (firstValue.Value < secondValue.Value)
                OnSecondBigger.Invoke();
        }
    }
}
