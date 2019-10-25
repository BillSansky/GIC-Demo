using UnityEngine;

namespace BFT
{
    [ExecuteInEditMode]

    public class TransformRotationPercentAngleLerp : MonoBehaviour
    {
        public bool LocalRotation = true;

        [SerializeField] private FloatValue maxPercentAngle;

        [SerializeField] private FloatValue minPercentAngle;

        public PercentValue Percent;

        public AxisBFT RotationAxis;

      

        void Update()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying && !DebugInEditMode)
                return;
#endif
            if (Percent != null)
                Rotate(Percent.Value);

#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                Rotate(DebugPercent);
            }
#endif
        }

        private void Rotate(float percentValue)
        {
            if (LocalRotation)
                transform.localRotation = Quaternion.Euler(MathExt.Axis(RotationAxis)
                                                           * Mathf.Lerp(minPercentAngle.Value, maxPercentAngle.Value, percentValue));
            else
                transform.rotation = Quaternion.Euler(MathExt.Axis(RotationAxis)
                                                      * Mathf.Lerp(minPercentAngle.Value, maxPercentAngle.Value, percentValue));
        }


#if UNITY_EDITOR

        public bool DebugInEditMode;

        [Range(0, 1)] public float DebugPercent;

#endif
    }
}
