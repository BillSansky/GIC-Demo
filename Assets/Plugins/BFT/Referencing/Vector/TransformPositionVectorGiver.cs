using UnityEngine;

namespace BFT
{
    public class TransformPositionVectorGiver : MonoBehaviour, IValue<Vector3>
    {
        public bool UseLocalPosition;

        public Vector3 Value => (UseLocalPosition) ? transform.localPosition : transform.position;
    }
}
