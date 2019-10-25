#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace BFT
{
    public class EditorDebug : MonoBehaviour
    {
        public string LogMessage;


        public void Log()
        {
            if (enabled)
                UnityEngine.Debug.Log(LogMessage, this);
        }

        public void Log(string log)
        {
            if (enabled)
                UnityEngine.Debug.Log(log, this);
        }

        public void Log(UnityEngine.Object obj)
        {
            if (enabled)
                UnityEngine.Debug.Log($"Logged Object {obj.name}", obj);
        }

        public void PauseApplication()
        {
            UnityEngine.Debug.Log($"Application pause by a debug script, {name}", this);
#if UNITY_EDITOR
            EditorApplication.isPaused = true;
#endif
        }
    }
}
#endif
