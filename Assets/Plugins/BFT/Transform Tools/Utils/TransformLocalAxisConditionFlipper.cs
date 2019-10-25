using System;
using UnityEngine;

namespace BFT
{
    public class TransformLocalAxisConditionFlipper : MonoBehaviour
    {
        private bool currentlyInverted;

        public BoolValue InversionCondition;
        public AxisBFT ToInvert;

        public void Invert()
        {
            currentlyInverted = !currentlyInverted;
            switch (ToInvert)
            {
                case AxisBFT.RIGHT:
                case AxisBFT.LEFT:
                    transform.localPosition = new Vector3(-transform.localPosition.x, transform.localPosition.y,
                        transform.localPosition.z);
                    break;
                case AxisBFT.UP:
                case AxisBFT.DOWN:
                    transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y,
                        transform.localPosition.z);
                    break;
                case AxisBFT.FORWARD:
                case AxisBFT.BACK:
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y,
                        -transform.localPosition.z);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void Update()
        {
            if ((InversionCondition.Value && currentlyInverted) ||
                (!InversionCondition.Value && !currentlyInverted))
            {
                Invert();
            }
        }
    }
}
