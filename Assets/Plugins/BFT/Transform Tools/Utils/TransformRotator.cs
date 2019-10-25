using UnityEngine;

namespace BFT
{
    [ExecuteAlways]
    public class TransformRotator : MonoBehaviour
    {
        public Vector3Value axis = new Vector3Value() {LocalValue = Vector3.forward};
        public FloatValue rotationSpeed;

        protected virtual void Update()
        {
            transform.Rotate(axis.Value, rotationSpeed.Value * UnityEngine.Time.deltaTime);

#if UNITY_EDITOR
            if (!Application.isPlaying)
                transform.Rotate(axis.Value, rotationSpeed.Value);
#endif
        }
    }
}
