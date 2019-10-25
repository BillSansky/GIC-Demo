using UnityEngine;

namespace BFT
{
    class EngineProperties : MonoBehaviour
    {
        public bool IsEditor => Application.isEditor;
        public bool IsDebugBuild => UnityEngine.Debug.isDebugBuild;
    }
}
