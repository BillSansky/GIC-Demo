using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    public class StringCompare : SerializedMonoBehaviour
    {
        [SerializeField] private StringValue firstValue;

        [SerializeField, BoxGroup("Events")] private UnityEvent OnDiffrent;

        [SerializeField, BoxGroup("Events")] private UnityEvent OnEqual;

        [SerializeField] private StringValue secondValue;

        [Button(ButtonSizes.Medium)]
        public void Compare()
        {
            if (firstValue.Value == secondValue.Value)
                OnEqual.Invoke();
            else
                OnDiffrent.Invoke();
        }
    }
}
