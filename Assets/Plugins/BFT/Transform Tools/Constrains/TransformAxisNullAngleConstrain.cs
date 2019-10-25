using UnityEngine;

namespace BFT
{
    public class TransformAxisNullAngleConstrain : AbstractTransformConstraint
    {
        public AxisBFT AxisToNullify;
        public float NullifyingSpeed;

        public override void Constrain()
        {
            transform.localRotation = Quaternion.Euler(Vector3.Lerp(transform.localEulerAngles,
                transform.localEulerAngles.Mult((Vector3.one - transform.LocalAxis(AxisToNullify)))
                , NullifyingSpeed * UnityEngine.Time.deltaTime));
        }
    }
}
