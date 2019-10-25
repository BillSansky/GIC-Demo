using UnityEngine;

namespace BFT
{
    [ExecuteInEditMode]
    public class TransformAxisProjectionConstrain : AbstractTransformConstraint
    {
        public bool LocalAxis;
        public AxisBFT ProjectionAxis;

        public TransformValue Reference;

        public Transform ToProject;

        public override void Constrain()
        {
            transform.position = MathExt.ProjectPointOnLine(Reference.position,
                (LocalAxis) ? Reference.Value.LocalAxis(ProjectionAxis) : MathExt.Axis(ProjectionAxis),
                ToProject.position);
        }
    }
}
