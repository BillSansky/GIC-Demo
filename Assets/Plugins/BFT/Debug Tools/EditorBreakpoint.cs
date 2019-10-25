using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

#endif
namespace BFT
{
    [AddComponentMenu("Debug/Editor BreakPoint")]
    public class EditorBreakpoint : MonoBehaviour
    {
        public bool Active = true;

#if UNITY_EDITOR
        public void PauseEditor()
        {
            if (Active)
                EditorApplication.isPaused = true;
        }
#endif
    }
}
