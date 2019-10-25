using System.Collections.Generic;
using UnityEngine;

namespace BFT
{
    /// <summary>
    ///     An object that belongs to a list.
    ///     Useful for registering the state of a scene
    ///     and record a change of location for an object, for instance.
    /// </summary>
    public abstract class ListedComponent<T, T1> : MonoBehaviour where T1 : List<T>
    {
        [SerializeField] private T1 listReference;
        public abstract T Data { get; }

        public T1 ListReference
        {
            get => listReference;
            set
            {
                listReference?.Remove(Data);
                listReference = value;
                listReference.Add(Data);
            }
        }

        void Reset()
        {
            FindFirstReferencer();
        }

        private void FindFirstReferencer()
        {
            if (listReference == null)
            {
                listReference = GetComponentInParent<T1>();
            }
        }

        void OnValidate()
        {
            FindFirstReferencer();
        }

        void Awake()
        {
            ListReference.Add(Data);
        }

        void OnDestroy()
        {
            ListReference.Remove(Data);
        }
    }
}
