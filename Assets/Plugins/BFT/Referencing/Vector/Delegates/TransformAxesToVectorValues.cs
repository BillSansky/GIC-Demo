using UnityEngine;

namespace BFT
{
    public class TransformAxesToVectorValues : MonoBehaviour
    {
        public TransformValue TransformToCopy;
        public Vector3Variable XAxis;
        public Vector3Variable YAxis;
        public Vector3Variable ZAxis;

        public void Update()
        {
            XAxis.Value = TransformToCopy.Value.right;
            YAxis.Value = TransformToCopy.Value.up;
            ZAxis.Value = TransformToCopy.forward;
        }
    }
}
