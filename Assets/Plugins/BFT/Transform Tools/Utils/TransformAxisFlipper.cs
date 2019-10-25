using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

//could expend the angle to any axis
namespace BFT
{
    public class TransformAxisFlipper : MonoBehaviour, IValue<bool>
    {
        public bool AbsoluteAxis;

        public AxisBFT Axis;
        private float currentTime = 0;

        private bool flipped = false;
        private bool flippingDone = true;

        public FloatValue InversionDuration;
        private Quaternion invertedRotation;
        private Quaternion rotationBeforeInversion;

        public UnityEngine.Transform ToInvert;

        public bool Value => flipped;

        public UnityEvent OnFlipStarted;
        public UnityEvent OnFlipEnded;

        public void InvertAngle()
        {
            currentTime = 0;
            flippingDone = false;
            rotationBeforeInversion = ToInvert.localRotation;
            ToInvert.Rotate(MathExt.Axis(Axis), 180, (AbsoluteAxis) ? Space.World : Space.Self);

            invertedRotation = ToInvert.localRotation;
            ToInvert.rotation = rotationBeforeInversion;

            OnFlipStarted.Invoke();
        }

        [Button]
        public void InstantFlip()
        {
            InvertAngle();
            ToInvert.localRotation = invertedRotation;
            flipped = !flipped;
            OnFlipEnded.Invoke();
        }

        void FixedUpdate()
        {
            if (flippingDone)
                return;
            currentTime += UnityEngine.Time.deltaTime;

            ToInvert.rotation = Quaternion.Slerp(rotationBeforeInversion, invertedRotation,
                currentTime / InversionDuration.Value);

            if (currentTime >= InversionDuration.Value)
            {
                flippingDone = true;
                flipped = !flipped;
                OnFlipEnded.Invoke();
            }
        }
    }
}
