using UnityEditor.Build;
using UnityEditor.Build.Reporting;
#if UNITY_EDITOR
using UnityEngine;

namespace BFT
{
    public class InspectorNoteManager : SingletonSO<InspectorNoteManager>, IProcessSceneWithReport
    {
        static InspectorNoteManager()
        {
            IsEditor = true;
        }

        public int callbackOrder => 0;

        public void OnProcessScene(UnityEngine.SceneManagement.Scene scene, BuildReport report)
        {
            foreach (GameObject rootGameObject in scene.GetRootGameObjects())
            {
                foreach (var note in rootGameObject.GetComponentsInChildren<InspectorNote>())
                {
                    DestroyImmediate(note);
                }
            }
        }
    }
}
#endif
