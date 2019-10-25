using UnityEngine;

namespace BFT
{
    public class TransformRotationSetter : MonoBehaviour
    {
        public Vector3Value AnglesToSet;

        public bool SetOnEnable;
        public UnityEngine.Transform ToSet;

        public void SetAngles()
        {
            ToSet.rotation = Quaternion.Euler(AnglesToSet.Value);
        }

        public void OnEnable()
        {
            if (SetOnEnable)
            {
                SetAngles();
            }
        }
    }
}
