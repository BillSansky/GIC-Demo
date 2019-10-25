using UnityEngine;

namespace BFT
{
    public class TransformConditionSwitchLerp : MonoBehaviour
    {
        private BoolValue copyA;

        public bool CopyRotation;
        public float RotationSpeed;

        public float Speed;

        public bool StartWithA;

        public TransformValue ToCopyA;
        public TransformValue ToCopyB;

        void Start()
        {
            copyA.LocalValue = StartWithA;
        }

        public void Update()
        {
            if (copyA.Value)
            {
                transform.position = Vector3.MoveTowards(transform.position, ToCopyA.position,
                    Speed * UnityEngine.Time.deltaTime);
                if (CopyRotation)
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, ToCopyA.rotation,
                        RotationSpeed * UnityEngine.Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, ToCopyB.position,
                    Speed * UnityEngine.Time.deltaTime);
                if (CopyRotation)
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, ToCopyB.rotation,
                        RotationSpeed * UnityEngine.Time.deltaTime);
            }
        }

        public void CopyA()
        {
            copyA.LocalValue = true;
            transform.position =
                Vector3.MoveTowards(transform.position, ToCopyA.position, Speed * UnityEngine.Time.deltaTime);
            if (CopyRotation)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, ToCopyA.rotation,
                    RotationSpeed * UnityEngine.Time.deltaTime);
        }

        public void CopyB()
        {
            copyA.LocalValue = false;
            transform.position =
                Vector3.MoveTowards(transform.position, ToCopyB.position, Speed * UnityEngine.Time.deltaTime);
            if (CopyRotation)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, ToCopyB.rotation,
                    RotationSpeed * UnityEngine.Time.deltaTime);
        }

        public void Switch()
        {
            copyA.LocalValue = !copyA.Value;
        }
    }
}
