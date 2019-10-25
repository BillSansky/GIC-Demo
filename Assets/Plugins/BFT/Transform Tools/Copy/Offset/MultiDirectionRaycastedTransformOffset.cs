using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    /// <summary>
    ///     Makes sure that the transform is always a certain distance to all directions
    /// </summary>
    public class MultiDirectionRaycastedTransformOffset : SerializedMonoBehaviour
    {
        public Vector3VariableAsset Dimensions;

        public LayerMask RaycastMask;

        public UnityEngine.Transform ReferenceTransform;
        public Vector3VariableAsset XAxis;
        public Vector3VariableAsset YAxis;
        public Vector3VariableAsset ZAxis;

        public void Update()
        {
            transform.position = ReferenceTransform.position;

            //TODO repeat the iteration until a balance is found, for X amount of times
            //TODO consider an offset in the raycast
            MoveTransformByRaycast(XAxis.Value, Dimensions.Value.x);
            MoveTransformByRaycast(YAxis.Value, Dimensions.Value.y);
            MoveTransformByRaycast(ZAxis.Value, Dimensions.Value.z);
            MoveTransformByRaycast(-XAxis.Value, Dimensions.Value.x);
            MoveTransformByRaycast(-YAxis.Value, Dimensions.Value.y);
            MoveTransformByRaycast(-ZAxis.Value, Dimensions.Value.z);
        }

        public void MoveTransformByRaycast(Vector3 direction, float minDistance)
        {
            Ray xRay = new Ray(transform.position, direction);
            RaycastHit hit;

            if (UnityEngine.Physics.Raycast(xRay, out hit, 9999, RaycastMask))
            {
                if (hit.distance < minDistance)
                {
                    transform.position = hit.point - xRay.direction * minDistance;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (!XAxis || !YAxis || !ZAxis)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + XAxis.Value);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + YAxis.Value);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + ZAxis.Value);
        }
    }
}
