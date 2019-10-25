using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;

namespace BFT
{
    public static class EditorUpdate
    {
        private static double lastTimeSinceStartup;

        private static List<System.Action<double>> editorActions = new List<System.Action<double>>();

        private static List<System.Action<double>> actionsToRemove = new List<System.Action<double>>();
        public static double EditorDeltaTime => EditorApplication.timeSinceStartup - lastTimeSinceStartup;


        public static void EditorUpdatePlug(System.Action<double> action)
        {
            if (editorActions.Contains(action))
                return;
            editorActions.Add(action);
        }

        public static void RemoveEditorUpdatePlug(System.Action<double> action)
        {
            actionsToRemove.Add(action);
        }

        [InitializeOnLoadMethod]
        private static void PlugUpdate()
        {
            EditorApplication.update -= UpdateLogic;
            EditorApplication.update += UpdateLogic;
            lastTimeSinceStartup = EditorApplication.timeSinceStartup;
        }

        private static void UpdateLogic()
        {
            foreach (var action in editorActions)
            {
                action(EditorDeltaTime);
            }

            foreach (var action in actionsToRemove)
            {
                editorActions.Remove(action);
            }

            actionsToRemove.Clear();

            lastTimeSinceStartup = EditorApplication.timeSinceStartup;
        }
    }
}

#endif
