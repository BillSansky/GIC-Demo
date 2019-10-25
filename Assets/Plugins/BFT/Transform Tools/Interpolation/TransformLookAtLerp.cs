using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class TransformLookAtLerp : UpdateTypeSwitchable
    {
        [BoxGroup("Lerp Options")] public bool ClampYAngle;

        [BoxGroup("Lerp Options")] public Vector3 Filter = Vector3.one;

        [BoxGroup("Lerp Options")] public float MaxYAngle;

        [BoxGroup("Lerp Options")] public FloatValue Speed;

        [BoxGroup("Look At")] public TransformValue ToLookAt;

        [BoxGroup("Lerp Options")] [SerializeField]
        private bool useUnscaledDeltaTime;

        public void Start()
        {
            transform.rotation.eulerAngles.Mult(Filter);
        }

        // Update is called once per frame
        public override void UpdateMethod()
        {
            if (ToLookAt != null)
            {
                Quaternion old = transform.rotation;
                transform.LookAt(ToLookAt.Value);
                if (ClampYAngle)
                {
                    float y = transform.eulerAngles.y;
                    if (y > 180)
                        y = -(360 - y);
                    y = Mathf.Clamp(y, -MaxYAngle, MaxYAngle);
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x * Filter.x, y * Filter.y,
                        transform.eulerAngles.z * Filter.z);
                }

                Quaternion newRot = Quaternion.Euler(transform.rotation.eulerAngles.Mult(Filter));

                transform.rotation = Quaternion.Lerp(old, newRot,
                    (useUnscaledDeltaTime ? UnityEngine.Time.unscaledDeltaTime : UnityEngine.Time.deltaTime) * Speed.Value);
            }
        }
    }
}
