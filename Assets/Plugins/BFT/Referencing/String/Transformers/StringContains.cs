using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    public class StringContains : SerializedMonoBehaviour
    {
        [SerializeField] bool ignoreCase;

        [SerializeField, BoxGroup("Events")] private UnityEvent OnContains;

        [SerializeField, BoxGroup("Events")] private UnityEvent OnNotContains;

        [SerializeField] private StringValue searchedValue;

        [SerializeField] private StringValue source;


        [Button(ButtonSizes.Medium)]
        public void Compare()
        {
            if (ignoreCase)
            {
                if (source.Value.ToLower().Contains(searchedValue.Value.ToLower()))
                    OnContains.Invoke();
                else
                    OnNotContains.Invoke();
            }
            else
            {
                if (source.Value.Contains(searchedValue.Value))
                    OnContains.Invoke();
                else
                    OnNotContains.Invoke();
            }
        }
    }
}
