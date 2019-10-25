using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    [ExecuteInEditMode]
    public class TransformVector3SegmentLerp : MonoBehaviour
    {
        [SerializeField, OnValueChanged("CalculatePoint")]
        private bool changeAngle = false;

        [SerializeField, Range(0, 1), OnValueChanged("CalculatePoint")]
        private PercentValue LerpPercent = new PercentValue(0.5f);

        [SerializeField, OnValueChanged("CalculatePoint")]
        private Vector3Value pointA, pointB;

        private void OnEnable()
        {
            CalculatePoint();
        }

        public void SetPoints(Vector3 pointA, Vector3 pointB)
        {
            this.pointA.LocalValue = pointA;
            this.pointB.LocalValue = pointB;

            CalculatePoint();
        }

        private void CalculatePoint()
        {
            CalculateNewPosition();
            CalculateNewAngle();
        }

        private void CalculateNewPosition()
        {
            transform.position = Vector3.Lerp(pointA.Value, pointB.Value, LerpPercent.Value);
        }

        private void CalculateNewAngle()
        {
            if (changeAngle)
            {
                Vector3 targetDir = pointB.Value - pointA.Value;
                if (targetDir != Vector3.zero)
                    transform.rotation = Quaternion.LookRotation(targetDir);
            }
            else
            {
                transform.localEulerAngles = Vector3.zero;
            }
        }
    }
}
