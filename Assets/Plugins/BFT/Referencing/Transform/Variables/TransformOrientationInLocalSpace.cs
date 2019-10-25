using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    /// <summary>
    ///     useful when you want to use a transform as a relative orientation to another one
    /// </summary>
    public class TransformOrientationInLocalSpace : MonoBehaviour, IOrientation, IValue<IOrientation>
    {
        public UnityEngine.Transform localTransform;
        public UnityEngine.Transform referenceTransform;

        [ShowInInspector] public Vector3 Up => referenceTransform.InverseTransformDirection(localTransform.up);

        [ShowInInspector] public Vector3 Right => referenceTransform.InverseTransformDirection(localTransform.right);

        [ShowInInspector] public Vector3 Forward => referenceTransform.InverseTransformDirection(localTransform.forward);
        public IOrientation Value => this;

        void Reset()
        {
            localTransform = transform;
            referenceTransform = transform.parent ? transform.parent : transform;
        }
    }
}
