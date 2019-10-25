using UnityEngine;

namespace BFT
{
    public class TransformWorldToScreen : MonoBehaviour
    {
        public Camera Cam;
        public TransformValue InWorld;


        // Update is called once per frame
        void Update()
        {
            transform.position = Cam.WorldToScreenPoint(InWorld.position).Mult(1, 1, 0);
        }
    }
}
