using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;

namespace BFT
{
    public static class BFTEditorUtils
    {
        private static List<System.Action> actionsToCallNextFrame = new List<System.Action>();

        public static void CallNextFrame(System.Action action)
        {
            actionsToCallNextFrame.Add(action);
            EditorApplication.update += NextFrameCallBack;
        }

        private static void NextFrameCallBack()
        {
            EditorApplication.update -= NextFrameCallBack;
            foreach (var action in actionsToCallNextFrame)
            {
                action();
            }

            actionsToCallNextFrame.Clear();
        }
    }
}
#endif