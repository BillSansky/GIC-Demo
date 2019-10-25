using System;
using UnityEngine;

namespace BFT
{
    [ExecuteInEditMode]
    public class TransformAxisDistanceClampConstrain : AbstractTransformConstraint
    {
        public AxisBFT Axis = AxisBFT.UP;

        private float currentDistance;
        public FloatValue DistanceDown;
        public FloatValue DistanceUp;

        public TransformValue Reference;

        public bool ResetLocalTransformEachFrame = true;

        public override void Constrain()
        {
            if (!Reference.Value)
                return;

            if (ResetLocalTransformEachFrame)
                transform.localPosition = Vector3.zero;

            switch (Axis)
            {
                case AxisBFT.RIGHT:
                    currentDistance = transform.position.x - Reference.position.x;
                    if (currentDistance < DistanceDown.Value)
                        transform.position.Set(DistanceDown.Value, transform.position.y, transform.position.z);
                    else if (currentDistance > DistanceUp.Value)
                        transform.position.Set(DistanceUp.Value, transform.position.y, transform.position.z);
                    break;
                case AxisBFT.UP:
                    currentDistance = transform.position.y - Reference.position.y;
                    if (currentDistance < DistanceDown.Value)
                        transform.position.Set(transform.position.x, DistanceDown.Value, transform.position.z);
                    else if (currentDistance > DistanceUp.Value)
                        transform.position.Set(transform.position.x, DistanceUp.Value, transform.position.z);
                    break;
                case AxisBFT.FORWARD:
                    currentDistance = transform.position.z - Reference.position.z;
                    if (currentDistance < DistanceDown.Value)
                        transform.position.Set(transform.position.x, transform.position.y, DistanceDown.Value);
                    else if (currentDistance > DistanceUp.Value)
                        transform.position.Set(transform.position.x, transform.position.y, DistanceUp.Value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
