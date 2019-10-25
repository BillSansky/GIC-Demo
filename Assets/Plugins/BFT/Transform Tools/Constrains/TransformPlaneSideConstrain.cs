using UnityEngine;

namespace BFT
{
    /// <summary>
    /// Constrains a transform so it remains on a specific plane on a specific side
    /// </summary>
    public class TransformPlaneSideConstrain : AbstractTransformConstraint
    {
        public TransformValue PlaneReference;
        public Transform ToConstrain;

        void Reset()
        {
            ToConstrain = transform;
        }

        public override void Constrain()
        {
            Vector3 projectedPosition = PlaneReference.Value.InverseTransformPoint(ToConstrain.position);
            if (projectedPosition.z < 0)
            {
                projectedPosition.z = 0;
                ToConstrain.position = PlaneReference.Value.TransformPoint(projectedPosition);
            }
        }

        public void OnDrawGizmosSelected()
        {
            if (!PlaneReference.Value)
                return;
            Gizmos.color = Color.grey.Alphaed(.5f);
            Gizmos.matrix = PlaneReference.Value.localToWorldMatrix;
            Gizmos.DrawCube(Vector3.back * .2f, new Vector3(500, 500, .4f));
        }
    }
}
