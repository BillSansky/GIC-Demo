using UnityEngine;

namespace BFT
{
    class TimeProperties : MonoBehaviour
    {
        public float Time => UnityEngine.Time.time;
        public float DeltaTime => UnityEngine.Time.deltaTime;
    }
}
