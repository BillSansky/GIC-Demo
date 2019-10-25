using UnityEngine;

namespace BFT
{
    public class TransformLookAtVectorSetter : MonoBehaviour
    {
        public Vector3Value targetVector;

        private void Update()
        {
            transform.LookAt(targetVector.Value);
        }
    }
}
