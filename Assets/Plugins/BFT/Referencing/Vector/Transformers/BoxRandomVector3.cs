using UnityEngine;

namespace BFT
{
    public class BoxRandomVector3 : MonoBehaviour, IValue<Vector3>
    {
        public BoxCollider Bounds;

        public Vector3 Value => Bounds.RandomPointInBox();
    }
}
