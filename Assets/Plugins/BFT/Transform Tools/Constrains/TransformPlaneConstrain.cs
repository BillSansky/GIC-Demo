using UnityEngine;

namespace BFT
{

    //constrain a transform so it remains on a specific plane
    public class TransformPlaneConstrain : AbstractTransformConstraint
    {
        public float HeightOffset = 0;

        public TransformValue Plane;
        public Transform ToProject;

        // Update is called once per frame
        public override void Constrain()
        {
            transform.position = GetPlanePosition();
        }


        public Vector3 GetPlanePosition()
        {
            return MathExt.ProjectPointOnPlane(Plane.up, Plane.position, ToProject.position) + HeightOffset * Plane.up;
        }
    }
}
