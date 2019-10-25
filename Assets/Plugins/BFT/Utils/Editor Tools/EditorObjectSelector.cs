using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
using System.Collections.Generic;

namespace BFT
{
    public class EditorObjectSelector : MonoBehaviour
    {
        [ListDrawerSettings(Expanded = true)] public List<SelectionObject> ObjectsToSelect = new List<SelectionObject>();

        [Button(ButtonSizes.Medium)]
        public void SelectAll()
        {
            List<UnityEngine.Object> objsToSelect = new List<UnityEngine.Object>(ObjectsToSelect.Count);
            foreach (var selectionObject in ObjectsToSelect)
            {
                objsToSelect.Add(selectionObject.Object);
            }

            Selection.objects = objsToSelect.ToArray();
        }

        [Button(ButtonSizes.Medium)]
        public void AddObjects(List<UnityEngine.Object> objs)
        {
            foreach (var o in objs)
            {
                ObjectsToSelect.Add(new SelectionObject() {Object = o});
            }
        }

        [Serializable]
        public class SelectionObject
        {
            public UnityEngine.Object Object;

            [Button(ButtonSizes.Medium)]
            public void SelectObject()
            {
                Selection.objects = new[] {Object};
                Selection.activeObject = Object;
                EditorGUIUtility.PingObject(Object);
            }
        }
    }
}

#endif
