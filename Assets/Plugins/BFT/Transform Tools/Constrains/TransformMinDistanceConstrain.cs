using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    /// <summary>
    ///     Ensures that a min distance is respected between two transforms
    /// </summary>

    public class TransformMinDistanceConstrain : AbstractTransformConstraint
    {
        [BoxGroup("Constraints")] public FloatValue MinDistance;

        [BoxGroup("Constraints")] public TransformValue Reference;

        public override void Constrain()
        {
#if UNITY_EDITOR
            if (!Reference.Value || MinDistance == null)
            {
                UnityEngine.Debug.LogWarningFormat(this,
                    "The reference transform or the distance are missing on {0}, disabling the component", gameObject.name);
                enabled = false;
                return;
            }
#endif

            if (Reference.Value.Distance(transform) < MinDistance.Value)
            {
                transform.position = Reference.position +
                                     transform.position.DistanceFromVector(Reference.position).normalized
                                     * MinDistance.Value;
            }
        }

#if UNITY_EDITOR

        [BoxGroup("Utils")] public Color DebugColor = Color.grey.Alphaed(.5f);

        public void OnDrawGizmosSelected()
        {
            if (!enabled)
                return;

            Gizmos.color = DebugColor;
            Gizmos.DrawSphere(Reference.position, MinDistance.Value);

            if (!Application.isPlaying)
                Constrain();
        }

        public void OnDrawGizmos()
        {
            if (!enabled)
                return;

            if (!Application.isPlaying)
                Constrain();
        }

#endif
    }
}
