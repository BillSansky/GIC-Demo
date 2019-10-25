using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

#endif

namespace BFT
{
    public class TransformCircleMappingConstrain : AbstractTransformConstraint, IValue<float>
    {
        public FloatValue AngleClamping = new FloatValue(180);

        [MinValue(0.001f)] public FloatValue CircleRadius = new FloatValue(1);

        public bool CircumferenceOnly;

        public AnimationCurve InterpolationToPositionCurve;

        public float MinPercent = 0;

        public bool PositionTransformedByReference = false;

        [Tooltip("The circle will be as if the up axis would be orthogonal to the plane holding the circle")]
        public TransformValue ReferencePoint;

        public FloatValue XPercent;


        private float xPosition;
        public FloatValue YPercent;
        private float zPosition;

        public float Percent => (transform.position - ReferencePoint.position).sqrMagnitude /
                                (CircleRadius.Value * CircleRadius.Value);

        public float Value => Percent;

        public override void Constrain()
        {
            if (XPercent == null || YPercent == null
                                 || (Mathf.Abs(XPercent.Value) < MinPercent
                                     && Mathf.Abs(YPercent.Value) < MinPercent))
                return;

            xPosition = XPercent.Value;
            zPosition = YPercent.Value;

            xPosition = Mathf.Sign(xPosition) * InterpolationToPositionCurve.Evaluate(Mathf.Abs(xPosition));
            zPosition = Mathf.Sign(zPosition) * InterpolationToPositionCurve.Evaluate(Mathf.Abs(zPosition));

            Vector3 position = new Vector3(xPosition * CircleRadius.Value, 0, zPosition * CircleRadius.Value);
            position = Vector3.ClampMagnitude(position, CircleRadius.Value);
            position = position.GetClampedAngleTo(ReferencePoint.forward, ReferencePoint.up, AngleClamping.Value);

            if (CircumferenceOnly && position.magnitude < CircleRadius.Value)
                position = position.normalized * CircleRadius.Value;

            transform.position = (PositionTransformedByReference)
                ? ReferencePoint.Value.TransformPoint(position)
                : ReferencePoint.position + (position);
        }

#if UNITY_EDITOR

        public Color DebugColor = Color.red;

        void OnDrawGizmosSelected()
        {
            if (!ReferencePoint.Value)
                return;
            Handles.color = DebugColor;

            Handles.matrix = ReferencePoint.Value.localToWorldMatrix;
            Handles.DrawSolidArc(Vector3.zero, Vector3.up, Vector3.forward, AngleClamping.Value, CircleRadius.Value);
            Handles.DrawSolidArc(Vector3.zero, Vector3.up, Vector3.forward, -AngleClamping.Value, CircleRadius.Value);
        }

        void OnDrawGizmos()
        {
            if (!Application.isPlaying)
                Constrain();
        }

#endif
    }
}
