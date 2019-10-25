using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    public class GameObjectReferenceComparisonBoolValue : MonoBehaviour, IValue<bool>
    {
        [BoxGroup("Objects"), SerializeField] private UnityEngine.GameObject objectToCompare;

        [BoxGroup("Events")] public UnityEvent OnDifferentObject;

        [BoxGroup("Events")] public UnityEvent OnSameObject;

        [BoxGroup("Objects"), SerializeField] private UnityEngine.GameObject referenceObject;

        public bool Value => referenceObject == objectToCompare;


        public void SetObjectToCompare(UnityEngine.GameObject obj)
        {
            objectToCompare = obj;
            if (Value)
                OnSameObject.Invoke();
            else
            {
                OnDifferentObject.Invoke();
            }
        }
    }
}
