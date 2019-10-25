using UnityEngine;

namespace BFT
{
    [CreateAssetMenu(menuName = "BFT/Profiles/Speed Profile", fileName = "Speed Profile")]
    public class SpeedProfile : ScriptableObject
    {
        public float MaxAccelerationForce = Mathf.Infinity;

        public float MaxDecelerationForce = Mathf.Infinity;
        public float MaxDistance = 10;

        public float MinDistance = 0;
        public AnimationCurve Profile = AnimationCurve.EaseInOut(0, 0, 1, 1);
        public float SpeedAtMaxDistance = 10;

        public float SpeedAtMinDistance = 0;

        public Vector3 ComputeRotationSpeed(UnityEngine.Transform transformToLerp, UnityEngine.Transform targetTransform, Vector3 lerpObjectSpeed)
        {
            return MathExt.GetLerpSpeed(transformToLerp.rotation.eulerAngles,
                targetTransform.rotation.eulerAngles, Profile,
                SpeedAtMinDistance, SpeedAtMaxDistance, MaxDistance, MinDistance
                , lerpObjectSpeed, MaxAccelerationForce, MaxDecelerationForce);
        }

        public Vector3 ComputeMovementSpeed(Vector3 position, Vector3 target, Vector3 lerpObjectSpeed)
        {
            return MathExt.GetLerpSpeed(position,
                target, Profile,
                SpeedAtMinDistance, SpeedAtMaxDistance, MaxDistance, MinDistance
                , lerpObjectSpeed, MaxAccelerationForce, MaxDecelerationForce);
        }

        public Vector3 ComputeMovementSpeed(UnityEngine.Transform transformToLerp, UnityEngine.Transform targetTransform, Vector3 lerpObjectSpeed,
            bool local = false)
        {
            return MathExt.GetLerpSpeed(local ? transformToLerp.localPosition : transformToLerp.position,
                local ? targetTransform.localPosition : targetTransform.position, Profile,
                SpeedAtMinDistance, SpeedAtMaxDistance, MaxDistance, MinDistance
                , lerpObjectSpeed, MaxAccelerationForce, MaxDecelerationForce);
        }

        public Vector3 ComputeForce(float mass, Vector3 velocity, Vector3 position, Vector3 target,
            float maxVelocity = Mathf.Infinity, float maxDownVelocityDifference = Mathf.Infinity,
            float maxUpVelocityDifference = Mathf.Infinity, float maxForce = Mathf.Infinity,
            Vector3 forceAxisNullifier = new Vector3(), bool logConstrains = false)
        {
            return PhysicsTools.GetPushForceNeeded(mass, velocity, position, target,
                maxVelocity, maxDownVelocityDifference,
                maxUpVelocityDifference, maxForce,
                forceAxisNullifier, logConstrains);
        }

        public Vector3 ComputeForce(Rigidbody body, Vector3 target,
            float maxVelocity = Mathf.Infinity, float maxDownVelocityDifference = Mathf.Infinity,
            float maxUpVelocityDifference = Mathf.Infinity, float maxForce = Mathf.Infinity,
            Vector3 forceAxisNullifier = new Vector3(), bool logConstrains = false)
        {
            return PhysicsTools.GetPushForceNeeded(body.mass, body.velocity, body.position, target,
                maxVelocity, maxDownVelocityDifference,
                maxUpVelocityDifference, maxForce,
                forceAxisNullifier, logConstrains);
        }
    }
}
