using UnityEngine;

namespace BFT
{

    public class TransformRotationLerp : UpdateTypeSwitchable
    {
        public bool Local;

        public FloatValue Speed;

        public TransformValue TransformToCopy;

        public override void UpdateMethod()
        {
            Interpolate();
        }

        private void Interpolate()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying && !TransformToCopy.Value)
                return;
#endif
            if (!Local)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, TransformToCopy.rotation, Speed.Value * UnityEngine.Time.deltaTime);
            else
            {
                transform.localRotation =
                    Quaternion.RotateTowards(transform.localRotation, TransformToCopy.localRotation, Speed.Value * UnityEngine.Time.deltaTime);
            }
#if UNITY_EDITOR

            if (!Application.isPlaying)
            {
                if (!Local)
                    transform.rotation = TransformToCopy.rotation;
                else
                {
                    transform.localRotation = TransformToCopy.localRotation;
                }
            }

#endif
        }
    }
}
