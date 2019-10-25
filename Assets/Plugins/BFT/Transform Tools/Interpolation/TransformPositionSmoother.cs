using UnityEngine;

namespace BFT
{
    public class TransformPositionSmoother : UpdateTypeSwitchable
    {
        private Vector3 previousPosition;
        public FloatValue SmoothSpeed;
        
        public void OnEnable()
        {
            previousPosition = transform.position;
        }

        public override void UpdateMethod()
        {
            transform.position = Vector3.Lerp(previousPosition, transform.position, SmoothSpeed.Value * UnityEngine.Time.deltaTime);
            previousPosition = transform.position;
        }
    }
}
