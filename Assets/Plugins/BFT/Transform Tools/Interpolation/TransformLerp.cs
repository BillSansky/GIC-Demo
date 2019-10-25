using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class TransformLerp : SerializedMonoBehaviour
    {
        public bool CalculateOnEnable = false;
        
        public bool LerpPosition = true;
        public bool LerpRotation = true;
        public bool LerpScale = true;

       public PercentValue Percent;

        public bool StartLerpOnEnable = false;

        public TransformValue StartTransform;
        public TransformValue EndTransform;
        
        public Transform TransformToLerp;

        public void OnEnable()
        {
            if (CalculateOnEnable)
                CalculateTransform();
            if (StartLerpOnEnable)
                StartLerping();
        }

        public void CalculateTransform()
        {
            if (LerpPosition)
            {
                TransformToLerp.position = Vector3.Lerp(StartTransform.position, EndTransform.position, Percent.Value);
            }

            if (LerpRotation)
            {
                TransformToLerp.rotation = Quaternion.Lerp(StartTransform.rotation, EndTransform.rotation, Percent.Value);
            }

            if (LerpScale)
            {
                TransformToLerp.localScale =
                    Vector3.Lerp(StartTransform.localScale, EndTransform.localScale, Percent.Value);
            }
        }

        public void StartLerping()
        {
            StartCoroutine(LerpOverTime());
        }

        public void StopLerping()
        {
            StopAllCoroutines();
        }

        private IEnumerator LerpOverTime()
        {
            while (true)
            {
                CalculateTransform();
                yield return null;
            }
        }
    }
}
