using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace BFT
{
    public class TransformDynamicLerp : UpdateTypeSwitchable
    {
        private Vector3 currentSpeed;

        [FoldoutGroup("Debug")] public Color DebugColor;

        [FormerlySerializedAs("CopyPositionOnEnable")]
        public bool InstantReachOnEnable = true;

        private bool lerping;
        public bool LerpOnStart = true;

        [TitleGroup("Target")] public bool LerpRotation;

        [TitleGroup("Target", "Scale")] public bool LerpScale;

        [TitleGroup("Target")] public bool LocalPosition;

        [TitleGroup("Target", "Rotation"), ShowIf("LerpRotation")]
        public bool LocalRotation;

        [TitleGroup("Acceleration")] public float MaxAcceleration = Mathf.Infinity;
        [TitleGroup("Acceleration")] public float MaxDeceleration = Mathf.Infinity;

        [TitleGroup("Speed")] [HideIf("UseSpeedProfileAsset")]
        public float MaxDistanceForSpeed = 10;

        [TitleGroup("Speed")] [HideIf("UseSpeedProfileAsset")]
        public float MinDistanceForSpeed = 0;

        [ShowIf("StopOnReach")] public float ReachThreshold;

        [TitleGroup("Target", "Rotation"), ShowIf("LerpRotation")]
        public float RotationSpeedMultiplier;

        [TitleGroup("Target", "Scale"), ShowIf("LerpScale")]
        public float ScaleSpeedMultiplier;

        [TitleGroup("Speed")] [HideIf("UseSpeedProfileAsset")]
        public float SpeedAtMaxDistance = 10;

        [TitleGroup("Speed")] [HideIf("UseSpeedProfileAsset")]
        public float SpeedAtMinDistance = 0;

        [TitleGroup("Speed")] [HideIf("UseSpeedProfileAsset")]
        public AnimationCurve SpeedProfile = AnimationCurve.EaseInOut(0, 0, 1, 1);

        [TitleGroup("Speed")] [ShowIf("UseSpeedProfileAsset")]
        public SpeedProfile SpeedProfileAsset;

        [TitleGroup("Target")] public bool StopOnReach;

        [TitleGroup("Target")] public UnityEngine.Transform Target;

        public UnityEngine.Transform ToLerp;

        //TODO: change parameters into a scriptable object

        [TitleGroup("Speed")] public bool UseSpeedProfileAsset = false;

        void Reset()
        {
            ToLerp = transform;
        }

        void Start()
        {
            if (LerpOnStart)
                lerping = true;
        }

        private void OnEnable()
        {
            if (InstantReachOnEnable)
                InstantReachTarget();
        }

        public void StartLerp()
        {
            lerping = true;
        }

        public void StartLerpAndSwitchTarget(UnityEngine.Transform target)
        {
            Target = target;
            StartLerp();
        }

        public void StopLerp()
        {
            lerping = false;
        }

        public override void UpdateMethod()
        {
            if (!lerping || !ToLerp || !Target)
                return;
#if UNITY_EDITOR
            if (!Application.isPlaying && UseSpeedProfileAsset && !SpeedProfileAsset)
                return;
#endif

            currentSpeed = LerpTransform(ToLerp, Target, currentSpeed);
            if (StopOnReach && (ToLerp.position - Target.position).IsMagnitudeUnder(ReachThreshold))
            {
                lerping = false;
            }
        }

        public Vector3 ComputeNeededSpeeds(UnityEngine.Transform transformToLerp, UnityEngine.Transform targetTransform, Vector3 lerpObjectSpeed)
        {
            return MathExt.GetLerpSpeed((!LocalPosition) ? transformToLerp.position : transformToLerp.localPosition,
                (!LocalPosition) ? targetTransform.position : targetTransform.localPosition, SpeedProfile,
                SpeedAtMinDistance, SpeedAtMaxDistance, MaxDistanceForSpeed,
                MinDistanceForSpeed, lerpObjectSpeed, MaxAcceleration, MaxDeceleration);
        }

        public void InstantReachTarget()
        {
            if (LocalPosition)
                ToLerp.localPosition = Target.localPosition;
            else
            {
                ToLerp.position = Target.position;
            }

            if (LerpRotation)
            {
                ToLerp.rotation = Target.rotation;
            }

            if (LerpScale)
            {
                ToLerp.localScale = Target.localScale;
            }
        }

        public Vector3 LerpTransform(UnityEngine.Transform transformToLerp, UnityEngine.Transform targetTransform, Vector3 lerpObjectSpeed)
        {
            if (UseSpeedProfileAsset)
                lerpObjectSpeed =
                    transformToLerp.LerpPosition(targetTransform, lerpObjectSpeed, SpeedProfileAsset, LocalPosition);
            else
                lerpObjectSpeed = transformToLerp.LerpPosition(targetTransform, SpeedProfile, LocalPosition,
                    SpeedAtMinDistance, SpeedAtMaxDistance, MaxDistanceForSpeed,
                    MinDistanceForSpeed, lerpObjectSpeed, MaxAcceleration, MaxDeceleration);

            if (LerpRotation)
            {
                if (LocalRotation)
                {
                    transformToLerp.localRotation = Quaternion.RotateTowards(transformToLerp.localRotation,
                        targetTransform.localRotation,
                        Mathf.Max(lerpObjectSpeed.magnitude, SpeedAtMinDistance) * RotationSpeedMultiplier *
                        UnityEngine.Time.deltaTime);
                }
                else
                {
                    transformToLerp.rotation = Quaternion.RotateTowards(transformToLerp.rotation, targetTransform.rotation,
                        Mathf.Max(lerpObjectSpeed.magnitude, SpeedAtMinDistance) * RotationSpeedMultiplier *
                        UnityEngine.Time.deltaTime);
                }
            }

            if (LerpScale)
            {
                transformToLerp.localScale = Vector3.MoveTowards(transformToLerp.localScale, targetTransform.localScale,
                    Mathf.Max(lerpObjectSpeed.magnitude, SpeedAtMinDistance) * ScaleSpeedMultiplier * UnityEngine.Time.deltaTime);
            }

            return lerpObjectSpeed;
        }

        public void OnDrawGizmosSelected()
        {
            if (!Target || !ToLerp)
                return;
            Gizmos.color = Color.white;

            Vector3 direction = Target.position - ToLerp.position;
            Gizmos.DrawLine(transform.position, Target.position);
            Gizmos.color = DebugColor.Inverted();
            Gizmos.DrawLine(Target.position, Target.position - direction.normalized * MaxDistanceForSpeed);
            Gizmos.color = DebugColor;
            Gizmos.DrawLine(Target.position, Target.position - direction.normalized * MinDistanceForSpeed);
        }
    }
}
