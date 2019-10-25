using System;
using System.Collections.Generic;
using UnityEngine;

namespace BFT
{
    public static class TransformExt
    {
        public static Vector3 LocalAxis(this UnityEngine.Transform transform, AxisBFT axis)
        {
            switch (axis)
            {
                case AxisBFT.RIGHT:
                    return transform.right;
                case AxisBFT.UP:
                    return transform.up;
                case AxisBFT.FORWARD:
                    return transform.forward;
                case AxisBFT.LEFT:
                    return -transform.right;
                case AxisBFT.DOWN:
                    return -transform.up;
                case AxisBFT.BACK:
                    return -transform.forward;
                default:
                    return Vector3.zero;
            }
        }

        public static Vector3 Axis(AxisBFT axis)
        {
            switch (axis)
            {
                case AxisBFT.RIGHT:
                    return Vector3.right;
                case AxisBFT.UP:
                    return Vector3.up;
                case AxisBFT.FORWARD:
                    return Vector3.forward;
                case AxisBFT.LEFT:
                    return -Vector3.right;
                case AxisBFT.DOWN:
                    return -Vector3.up;
                case AxisBFT.BACK:
                    return -Vector3.forward;
                default:
                    return Vector3.zero;
            }
        }

        public static void AngleDurationRotate(this UnityEngine.Transform trans, Vector3 initialEuler, AxisBFT rotationAxis,
            float finalAngle, float durationPercent, bool localRotation = true, bool localAxis = true)
        {
            Vector3 finalEulerAngles =
                ((localAxis) ? LocalAxis(trans, rotationAxis) : MathExt.Axis(rotationAxis)) * finalAngle;

            if (localRotation)
                trans.localRotation = Quaternion.Euler(Vector3.Lerp(initialEuler, finalEulerAngles, durationPercent));
            else
                trans.rotation = Quaternion.Euler(Vector3.Lerp(initialEuler, finalEulerAngles, durationPercent));
        }

        public static void AngleEasedSpeedRotate(this UnityEngine.Transform trans, AxisBFT rotationAxis, float finalAngle, float speed,
            bool localRotation = true, bool localAxis = true)
        {
            Vector3 finalEulerAngles =
                ((localAxis) ? LocalAxis(trans, rotationAxis) : MathExt.Axis(rotationAxis)) * finalAngle;

            if (localRotation)
                trans.localRotation =
                    Quaternion.Euler(Vector3.Lerp(trans.localEulerAngles, finalEulerAngles, speed * UnityEngine.Time.deltaTime));
            else
                trans.rotation =
                    Quaternion.Euler(Vector3.Lerp(trans.eulerAngles, finalEulerAngles, speed * UnityEngine.Time.deltaTime));
        }

        public static void ZeroLocals(this UnityEngine.Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        public static void BackToDefaults(this UnityEngine.Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            transform.localRotation = Quaternion.identity;
        }

        public static void Copy(this UnityEngine.Transform trans, UnityEngine.Transform transform, bool copyPosition = true,
            bool copyRotation = false, bool copyLocalScale = false)
        {
            if (copyPosition)
                trans.position = transform.position;
            if (copyRotation)
                trans.rotation = transform.rotation;
            if (copyLocalScale)
                trans.localScale = transform.localScale;
        }

        public static void CopyPositionRotation(this UnityEngine.Transform transform, UnityEngine.Transform other)
        {
            transform.position = other.position;
            transform.rotation = other.rotation;
        }

        public static void CopyLocalPositionRotation(this UnityEngine.Transform transform, UnityEngine.Transform other)
        {
            transform.localPosition = other.localPosition;
            transform.localRotation = other.localRotation;
        }

        public static float Distance(this UnityEngine.Transform trans, UnityEngine.Transform trans2)
        {
            return (trans.position - trans2.position).magnitude;
        }

        public static bool IsDistanceUnder(this UnityEngine.Transform trans, UnityEngine.Transform trans2, float distanceThreshold)
        {
            return (trans.position - trans2.position).IsMagnitudeUnder(distanceThreshold);
        }

        public static Vector3 DirectionTo(this UnityEngine.Transform trans, UnityEngine.Transform trans2)
        {
            return (trans2.position - trans.position).normalized;
        }

        public static void LerpLookAt(this UnityEngine.Transform self, UnityEngine.Transform from, UnityEngine.Transform to, float t)
        {
            self.LookAt(Vector3.Lerp(from.position, to.position, t));
        }

        public static void LerpPositionRotation(this UnityEngine.Transform self, UnityEngine.Transform from, UnityEngine.Transform to, float t,
            bool local = false)
        {
            if (!local)
            {
                self.position = Vector3.Lerp(from.position, to.position, t);
                self.rotation = Quaternion.Lerp(from.rotation, to.rotation, t);
            }
            else
            {
                self.localPosition = Vector3.Lerp(from.localPosition, to.localPosition, t);
                self.localRotation = Quaternion.Lerp(from.localRotation, to.localRotation, t);
            }
        }

        public static IEnumerable<UnityEngine.Transform> DirectChildren(this UnityEngine.Transform self)
        {
            List<UnityEngine.Transform> trans = new List<UnityEngine.Transform>(self.childCount);
            for (int i = 0; i < self.childCount; i++)
            {
                trans.Add(self.GetChild(i));
            }

            return trans;
        }

        public static IEnumerator<UnityEngine.Transform> DirectChildrenEnum(this UnityEngine.Transform self)
        {
            for (int i = 0; i < self.childCount; i++)
            {
                yield return (self.GetChild(i));
            }
        }

        public static void ForEachChildrenGameObject(this GameObject go, System.Action<GameObject> action)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                action(go.transform.GetChild(i).gameObject);
            }
        }

        /// <summary>
        ///     Lerps the transform according to all parameters, and returns the speed given to the transform
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="target"></param>
        /// <param name="speedProfile"></param>
        /// <param name="local"></param>
        /// <param name="lowDistanceSpeed"></param>
        /// <param name="highDistanceSpeed"></param>
        /// <param name="highDistance"></param>
        /// <param name="lowDistance"></param>
        /// <param name="currentSpeed"></param>
        /// <param name="maxAcceleration"></param>
        /// <param name="maxDeceleration"></param>
        /// <returns></returns>
        public static Vector3 LerpPosition(this UnityEngine.Transform transform, UnityEngine.Transform target, AnimationCurve speedProfile,
            bool local = false,
            float lowDistanceSpeed = 0, float highDistanceSpeed = 10, float highDistance = 10, float lowDistance = 0,
            Vector3 currentSpeed = new Vector3(), float maxAcceleration = Mathf.Infinity,
            float maxDeceleration = Mathf.Infinity)
        {
            Vector3 speed = MathExt.GetLerpSpeed((local) ? transform.localPosition : transform.position,
                (local) ? target.localPosition : target.position, speedProfile, lowDistanceSpeed, highDistanceSpeed,
                highDistance, lowDistance, currentSpeed, maxAcceleration, maxDeceleration);

            if (local)
            {
                transform.localPosition += speed;
            }
            else
            {
                transform.position += speed;
            }

            return speed;
        }

        public static Vector3 LerpPosition(this UnityEngine.Transform transform, UnityEngine.Transform target, Vector3 currentSpeed,
            SpeedProfile profile,
            bool local = false)
        {
            return LerpPosition(transform, target, profile.Profile, local, profile.SpeedAtMinDistance,
                profile.SpeedAtMaxDistance, profile.MaxDistance, profile.MinDistance, currentSpeed,
                profile.MaxAccelerationForce, profile.MaxDecelerationForce);
        }


        public static List<UnityEngine.Transform> GetAllSubTransforms(this UnityEngine.Transform t, bool includeSelf = false,
            Func<UnityEngine.Transform, bool> condition = null)
        {
            List<UnityEngine.Transform> transforms = new List<UnityEngine.Transform>();
            if (includeSelf)
            {
                if (condition == null || condition(t))
                    transforms.Add(t);
            }

            foreach (UnityEngine.Transform transform in t)
            {
                transforms.AddRange(transform.GetAllSubTransforms(true, condition));
            }

            return transforms;
        }
    }
}
