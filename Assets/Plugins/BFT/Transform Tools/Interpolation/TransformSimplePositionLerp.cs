using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace BFT
{
    [ExecuteInEditMode]
    public class TransformSimplePositionLerp : UpdateTypeSwitchable
    {
        [BoxGroup("Lerp Options")] public float AcceptableDistance = 0;

        [BoxGroup("Lerp Options"), PreviouslySerializedAs("Lerp")]
        public BoolValue LerpCondition;

        [BoxGroup("Lerp Options")] public bool LerpLocalValues;

        [BoxGroup("Lerp")] public float Speed = 1;

        [BoxGroup("Lerp Options")] public bool TeleportOnEnable = true;

        [BoxGroup("Lerp")] public TransformValue LerpTarget;

        [BoxGroup("Lerp Options")] public bool UnscaledDelta = true;

        public float Distance
        {
            get => (LerpTarget.position - transform.position).magnitude;
            set =>
                transform.position = LerpTarget.position - value *
                                     (LerpTarget.position - transform.position).normalized;
        }

        void OnEnable()
        {
#if UNITY_EDITOR
            if (!LerpTarget.Value)
                return;
#endif

            if (TeleportOnEnable)
            {
                Teleport();
            }
        }

        public void Teleport()
        {
            if (LerpLocalValues)
                transform.localPosition = LerpTarget.localPosition;
            else
            {
                transform.position = LerpTarget.position;
            }
        }

        public override void UpdateMethod()
        {
            if (LerpTarget.Value)
            {
                if (LerpCondition != null && !LerpCondition.Value)
                    return;

                if (LerpLocalValues)
                {
                    if (!Mathf.Approximately(0, AcceptableDistance) &&
                        transform.localPosition.IsDistanceUnder(LerpTarget.localPosition, AcceptableDistance))
                        return;

                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, LerpTarget.localPosition
                        , Speed * ((UnscaledDelta) ? UnityEngine.Time.unscaledTime : UnityEngine.Time.deltaTime));
                }
                else
                {
                    if (!Mathf.Approximately(0, AcceptableDistance) &&
                        transform.position.IsDistanceUnder(LerpTarget.position, AcceptableDistance))
                        return;

                    transform.position = Vector3.MoveTowards(transform.position, LerpTarget.position
                        , Speed * ((UnscaledDelta) ? UnityEngine.Time.unscaledTime : UnityEngine.Time.deltaTime));
                }
            }

#if UNITY_EDITOR
            if (!Application.isPlaying && LerpTarget.Value)
                transform.position = LerpTarget.position;
#endif
        }
    }
}
