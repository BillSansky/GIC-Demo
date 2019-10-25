using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class TransformScaleSetter : MonoBehaviour
    {
        [HideIf("UseVectorScale")] public FloatValue Scale;

        public bool ScaleEveryFrame;

        public bool ScaleOnEnable = true;
        
        public Transform Target;
        
        public bool UseVectorScale = false;

        public bool ValueIsHalfSize = true;

        [ShowIf("UseVectorScale")]
        public FloatValue X, Y, Z;
        

        public void Reset()
        {
            Target = transform;
        }

        private void OnEnable()
        {
            if (!Target)
                Target = transform;

            if (ScaleOnEnable)
                SetScale();

            if (ScaleEveryFrame)
                StartScaleEveryFrame();
        }

        public void StartScaleEveryFrame()
        {
            StartCoroutine(ScaleEveryFrameCoroutine());
        }

        public void StopScaleEveryFrame()
        {
            StopAllCoroutines();
        }

        private IEnumerator ScaleEveryFrameCoroutine()
        {
            while (true)
            {
                SetScale();
                yield return null;
            }
        }

        [Button(ButtonSizes.Medium)]
        public void SetScale()
        {
            
            Vector3 size = UseVectorScale
                ?  new Vector3(GetAxisValue(X), GetAxisValue(Y), GetAxisValue(Z)) 
                : new Vector3(Scale.Value, Scale.Value, Scale.Value);

            if (ValueIsHalfSize)
            {
                size *= 2;
            }

            Target.localScale = size;
        }

        private float GetAxisValue(IValue<float> axis)
        {
            return axis != null ? axis.Value : 1;
        }
    }
}
