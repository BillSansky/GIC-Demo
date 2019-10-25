using UnityEngine;

namespace BFT
{
    public class TransformRotatingSpeedLerper : TransformRotator
    {
        public FloatValue MaxRotationSpeed;

        public FloatValue MinRotationSpeed;

        [SerializeField] public PercentValue Percent;

        public AnimationCurve SpeedProfile;


        protected override void Update()
        {
            if (Percent != null)
                rotationSpeed.LocalValue = Mathf.Lerp(MinRotationSpeed.Value, MaxRotationSpeed.Value, SpeedProfile.Evaluate(Percent.Value));
            base.Update();
        }
    }
}
