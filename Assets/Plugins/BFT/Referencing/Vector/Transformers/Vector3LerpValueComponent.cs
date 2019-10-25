using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class Vector3LerpValueComponent : MonoBehaviour, IValue<Vector3>
    {
        [ShowInInspector, ReadOnly, BoxGroup("Status")]
        private Vector3 currentValue;

        public FloatValue LerpingSpeed = new FloatValue() {LocalValue = 0.1f};

        public bool SetValueOnEnable = true;
        public Vector3Value Vector3;

        public Vector3 Value => currentValue;

        public void OnEnable()
        {
            if (SetValueOnEnable)
                currentValue = Vector3.Value;
        }

        public void Update()
        {
            Vector3 distance = (Vector3.Value - currentValue);
            currentValue += Mathf.Min(distance.magnitude, LerpingSpeed.Value * UnityEngine.Time.deltaTime) * distance.normalized;
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = UnityEngine.Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + currentValue);
            HandleExt.Text(transform.position + currentValue, $"Current value: {currentValue}", true);
        }
    }
}
