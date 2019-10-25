using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class UnflagScript : MonoBehaviour
    {
        [Button]
        private void Unflag()
        {
            GameObject root = gameObject;
            while (root.transform.parent)
            {
                root = root.transform.parent.gameObject;
            }

            Unflag(root);
        }

        private static void Unflag(GameObject root)
        {
            root.hideFlags = HideFlags.None;

            foreach (var component in root.GetComponents<Component>())
            {
                if (component)
                {
                    component.hideFlags = HideFlags.None;
                }
            }

            foreach (UnityEngine.Transform children in root.transform)
            {
                Unflag(children.gameObject);
            }
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("BFT/Utils/Unflag Active Scene")]
        private static void UnflagActiveScene()
        {
            var rootObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (var rootObject in rootObjects)
            {
                Unflag(rootObject);
            }
        }
#endif
    }
}
