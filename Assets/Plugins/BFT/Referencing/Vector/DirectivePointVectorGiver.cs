using UnityEngine;

namespace BFT
{
    public class DirectivePointVectorGiver : MonoBehaviour, IValue<Vector3>
    {
        public Vector3 Direction;

        public bool LocalDirection;

        public UnityEngine.Transform Reference;

        public Vector3 Value =>
            (LocalDirection) ? Reference.InverseTransformPoint(Direction) : Reference.position + Direction;

        public void OnDrawGizmosSelected()
        {
            Gizmos.DrawLine(Reference.position,
                Value);
        }
    }
}
