using System.Collections.Generic;
using UnityEngine;

namespace BFT
{
    public static class PhysicsTools
    {
        public delegate bool DOrtogonalRaycastCondition(RaycastHit hit);

        public static void TeleportRigidBody(Rigidbody body, Vector3 position, bool motionKept = true,
            List<Rigidbody> jointAttached = null, bool useTransform = false)
        {
            Vector3 intialBodyPosition = body.position;
            Vector3 velocity = Vector3.zero;
            Quaternion rotation = Quaternion.identity;
            Vector3 angularVelocity = Vector3.zero;

            if (motionKept)
            {
                velocity = body.velocity;
                rotation = body.rotation;
                angularVelocity = body.angularVelocity;
            }

            RigidbodyConstraints constrains = body.constraints;

            body.isKinematic = true;
            body.freezeRotation = true;

            body.position = position;

            body.isKinematic = false;
            body.freezeRotation = false;
            body.constraints = constrains;

            if (motionKept)
            {
                body.velocity = velocity;
                body.rotation = rotation;
                body.angularVelocity = angularVelocity;
            }
            else
            {
                body.velocity = Vector3.zero;
                body.rotation = Quaternion.identity;
                body.angularVelocity = Vector3.zero;
            }

            if (jointAttached != null)
            {
                foreach (Rigidbody attachedByJoint in jointAttached)
                {
                    TeleportRigidBody(attachedByJoint, position + (attachedByJoint.position - intialBodyPosition),
                        motionKept);
                }
            }
        }

        public static bool IsLayerInMask(LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }

        public static void PreTeleportation(Rigidbody body, List<Rigidbody> jointAttached, bool motionKept,
            out PreTeleportationValues bodyValues, out PreTeleportationValues[] attachedBodyValues)
        {
            bodyValues = new PreTeleportationValues(body, motionKept);
            body.isKinematic = true;
            body.freezeRotation = true;

            attachedBodyValues = null;

            if (jointAttached != null)
            {
                List<PreTeleportationValues> preValues = new List<PreTeleportationValues>(jointAttached.Count);

                foreach (Rigidbody attachedByJoint in jointAttached)
                {
                    PreTeleportationValues jointValue;
                    PreTeleportationValues[] useless;
                    PreTeleportation(attachedByJoint, null, motionKept, out jointValue, out useless);
                    preValues.Add(jointValue);
                }

                attachedBodyValues = preValues.ToArray();
            }
        }

        public static void PostTeleportation(Rigidbody body, List<Rigidbody> jointAttached, bool motionKept,
            PreTeleportationValues bodyValues, PreTeleportationValues[] attachedBodyValues)
        {
            body.isKinematic = false;
            body.freezeRotation = false;
            body.constraints = bodyValues.constrains;

            if (motionKept)
            {
                body.velocity = bodyValues.velocity;
                body.rotation = bodyValues.rotation;
                body.angularVelocity = bodyValues.angularVelocity;
            }
            else
            {
                body.velocity = Vector3.zero;
                body.rotation = Quaternion.identity;
                body.angularVelocity = Vector3.zero;
            }

            if (jointAttached != null)
            {
                int i = 0;
                foreach (Rigidbody attachedByJoint in jointAttached)
                {
                    PostTeleportation(attachedByJoint, null, motionKept, attachedBodyValues[i], null);
                    i++;
                }
            }
        }

        public static Vector3 GetPushForceNeeded(Rigidbody body, Vector3 target,
            float maxVelocity = Mathf.Infinity, float maxDownVelocityDifference = Mathf.Infinity,
            float maxUpVelocityDifference = Mathf.Infinity, float maxForce = Mathf.Infinity,
            Vector3 forceAxisNullifier = new Vector3(), bool logConstrains = false)
        {
            return GetPushForceNeeded(body.mass, body.velocity, body.position, target,
                maxVelocity, maxDownVelocityDifference, maxUpVelocityDifference,
                maxForce, forceAxisNullifier, logConstrains);
        }

        public static Vector3 GetPushForceNeeded(float mass, Vector3 velocity, Vector3 position, Vector3 target,
            float maxVelocity = Mathf.Infinity, float maxDownVelocityDifference = Mathf.Infinity,
            float maxUpVelocityDifference = Mathf.Infinity, float maxForce = Mathf.Infinity,
            Vector3 forceAxisNullifier = new Vector3(), bool logConstrains = false)
        {
            Vector3 outVelocity = target - position;

#if UNITY_EDITOR
            //Logging the velocity difference change
            if (logConstrains)
            {
                if (outVelocity.magnitude < velocity.magnitude - maxDownVelocityDifference)
                    UnityEngine.Debug.Log("Velocity Down Difference Clamped, Expected: " + outVelocity.magnitude
                                                                             + " Clamped: " +
                                                                             (velocity.magnitude - maxDownVelocityDifference
                                                                             ));

                if (outVelocity.magnitude > velocity.magnitude + maxUpVelocityDifference)
                    UnityEngine.Debug.Log("Velocity Up Difference Clamped, Expected: " + outVelocity.magnitude
                                                                           + " Clamped: " +
                                                                           (velocity.magnitude + maxUpVelocityDifference));
            }
#endif
            //clamping the velocity difference
            if (maxUpVelocityDifference > 0)
                outVelocity = outVelocity.GetClampedMagnitude(
                    velocity.magnitude - maxDownVelocityDifference,
                    velocity.magnitude + maxUpVelocityDifference);

            //clamping the max velocity
            outVelocity = Vector3.ClampMagnitude(outVelocity, maxVelocity * UnityEngine.Time.deltaTime);

#if UNITY_EDITOR
            if (logConstrains)
            {
                if ((target - position).magnitude > maxVelocity)
                    UnityEngine.Debug.Log("Max Velocity Clamped, Expected: "
                              + (target - position).magnitude + "Clamped: " + maxVelocity);
            }
#endif
            //Calculating the force required
            outVelocity = (outVelocity / TimeUtils.safeDeltaTime - velocity) / TimeUtils.safeDeltaTime
                          * mass;


#if UNITY_EDITOR
            //Logging the max force clamping
            if (logConstrains)
            {
                if (!(outVelocity.magnitude <= maxForce))
                    UnityEngine.Debug.Log("Max Force Clamped, Expected: "
                              + outVelocity.magnitude + " Clamped: " + maxForce);
            }
#endif
            //Clamping the max force
            outVelocity = Vector3.ClampMagnitude(outVelocity, maxForce);

            //removing undesired axis
            if (forceAxisNullifier != Vector3.zero)
                outVelocity = MathExt.ProjectVectorOnPlane(outVelocity, forceAxisNullifier);

            return outVelocity;
        }

        public static void TargetPosition(Rigidbody body, Vector3 target,
            float maxVelocity = Mathf.Infinity, float maxDownVelocityDifference = Mathf.Infinity,
            float maxUpVelocityDifference = Mathf.Infinity, float maxForce = Mathf.Infinity,
            Vector3 forceAxisNullifier = new Vector3(), bool logConstrains = false)
        {
            body.AddForce(GetPushForceNeeded(body.mass, body.velocity, body.position, target,
                maxVelocity, maxDownVelocityDifference, maxUpVelocityDifference, maxForce,
                forceAxisNullifier, logConstrains));
        }

        public static void TargetLookAt(Rigidbody body, Vector3 lookDirection, Vector3 normal
            , float maxAngularVelocityDifference = 0, float maxTorque = Mathf.Infinity)
        {
            Quaternion desiredQuat = Quaternion.LookRotation(lookDirection, normal);
            var z = Vector3.Cross(body.transform.forward, desiredQuat * Vector3.forward);
            var y = Vector3.Cross(body.transform.up, desiredQuat * Vector3.up);
            var thetaZ = Mathf.Asin(z.magnitude);
            var thetaY = Mathf.Asin(y.magnitude);

            var dt = TimeUtils.safeDeltaTime;
            var w = Vector3.zero;

            if (!Mathf.Approximately(dt, 0))
            {
                var wZ = z.normalized * (thetaZ / dt);
                var wY = y.normalized * (thetaY / dt);
                w = wZ + wY;
                w -= body.angularVelocity;
                w /= dt;
            }

            if (!Mathf.Approximately(maxAngularVelocityDifference, 0))
            {
                w = w.GetClampedMagnitude(body.angularVelocity.magnitude
                                          - maxAngularVelocityDifference,
                    body.angularVelocity.magnitude + maxAngularVelocityDifference);
            }

            w = Vector3.ClampMagnitude(w, maxTorque);


            var q = body.rotation * body.inertiaTensorRotation;
            var T = q * Vector3.Scale(body.inertiaTensor, Quaternion.Inverse(q) * (w));

            if (!float.IsNaN(T.x))
                body.AddTorque(T);
        }

        public static bool OrtogonalRaycasts(Vector3 source, Vector3 destination, float castSegmentLength,
            float castHalfDistance, DOrtogonalRaycastCondition condition)
        {
            float length = (destination - source).magnitude;

            float resolution = castSegmentLength / (destination - source).magnitude;
            float castPointRatio = 0;

            while (castPointRatio < 1f)
            {
                RaycastHit hit;

                Vector3 raycastDestination = Vector3.Lerp(source, destination, castPointRatio);

                if (UnityEngine.Physics.Raycast(raycastDestination + castHalfDistance * Vector3.up, -Vector3.up, out hit,
                    2 * castHalfDistance))
                {
                    if (condition(hit))
                        return true;
                }

                castPointRatio += resolution;
            }

            return false;
        }

        public struct PreTeleportationValues
        {
            public Vector3 initialBodyPosition;
            public Vector3 velocity;
            public Quaternion rotation;
            public Vector3 angularVelocity;
            public RigidbodyConstraints constrains;

            public PreTeleportationValues(Rigidbody body, bool motionKept)
            {
                initialBodyPosition = body.position;
                velocity = Vector3.zero;
                rotation = Quaternion.identity;
                angularVelocity = Vector3.zero;
                constrains = body.constraints;

                if (motionKept)
                {
                    velocity = body.velocity;
                    rotation = body.rotation;
                    angularVelocity = body.angularVelocity;
                }
            }
        }
    }
}
