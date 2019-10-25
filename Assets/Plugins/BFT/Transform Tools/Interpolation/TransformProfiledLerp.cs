using UnityEngine;

namespace BFT
{
    public class TransformProfiledLerp : UpdateTypeSwitchable
    {
        public Color DebugColor = Color.blue;

        public float MaxDistanceToConsider = 1000;
        public float MinDistanceToConsider = 0;
        public float SpeedAtMaxDistance = 10;
        public float SpeedAtMinDistance = 0;

        public AnimationCurve SpeedProfile;

        public TransformValue ToFollow;

        public override void UpdateMethod()
        {
            Vector3 distance = ToFollow.position - transform.position;
            transform.Translate(MathExt.EvaluateCurve(SpeedAtMinDistance, SpeedAtMaxDistance, SpeedProfile,
                                    distance.magnitude, MinDistanceToConsider, MaxDistanceToConsider) *
                                UnityEngine.Time.deltaTime * distance.normalized);
        }

        public void OnDrawGizmosSelected()
        {
            if (ToFollow.Value)
            {
                Vector3 dir = ToFollow.Value.DirectionTo(transform);
                Gizmos.color = Color.white;
                Gizmos.DrawLine(ToFollow.position, transform.position);
                Gizmos.color = DebugColor;
                Gizmos.DrawLine(ToFollow.position, ToFollow.position + dir * MaxDistanceToConsider);
                Gizmos.color = DebugColor.Inverted();
                Gizmos.DrawLine(ToFollow.position, ToFollow.position + dir * MinDistanceToConsider);
            }
        }
    }
}
