using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;
using Image = UnityEngine.UIElements.Image;

namespace BFT
{
    public class ObjectCallBacks : SerializedMonoBehaviour
    {
        public void DestroyGameObject(float timeOut)
        {
            Destroy(gameObject, timeOut);
        }

        public void DestroyCollidingGameObject(Collider collider)
        {
            Destroy(collider.gameObject);
        }

        public void DisableCollidingGameObject(Collider collider)
        {
            collider.gameObject.SetActive(false);
        }

        public void EnableObject(GameObject gameObject)
        {
            gameObject.SetActive(true);
        }

        public void Unparent()
        {
            gameObject.transform.SetParent(null);
        }

        public void SetParentOrDestroyIfNull(UnityEngine.Transform parent)
        {
            if (parent == null)
                Destroy(gameObject);
            else
                gameObject.transform.SetParent(parent);
        }

        public void SetLocalPositionToZero()
        {
            gameObject.transform.localPosition = Vector3.zero;
        }

        public void DisableObject(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }

        public void EnableSelectable(Selectable selectable)
        {
            selectable.interactable = true;
        }

        public void DisableSelectable(Selectable selectable)
        {
            selectable.interactable = false;
        }

        public void ChangeLineRendererSharedMaterial(Material material)
        {
            gameObject.GetComponent<LineRenderer>().sharedMaterial = material;
        }

        public void ConsoleDebugWarning(string text)
        {
            UnityEngine.Debug.LogWarning(text, this);
        }

     

        public void DisableColliders()
        {
            DisableColliders(transform);
        }

        public void DisableColliders(UnityEngine.Transform parent)
        {
            var colliders = parent.GetComponents<Collider>();
            foreach (var c in colliders)
            {
                c.enabled = false;
            }

            foreach (UnityEngine.Transform children in parent)
            {
                DisableColliders(children);
            }
        }

        public void ApplyMaterial(Material material)
        {
            ApplyMaterial(material, transform);
        }

        public void ApplyMaterial(Material material, UnityEngine.Transform transform)
        {
            var renderers = transform.GetComponents<Renderer>();
            foreach (var r in renderers)
            {
                r.material = material;
            }

            var images = transform.GetComponents<Image>();
        /*    foreach (var i in images)
            {
                i.material = material;
            }*/

            foreach (UnityEngine.Transform children in transform)
            {
                ApplyMaterial(material, children);
            }
        }

        public void SetLayerToChildren(int layer)
        {
            SetLayerToChildren(layer, transform);
        }

        public void SetLayerToChildren(int layer, UnityEngine.Transform transform)
        {
            transform.gameObject.layer = layer;
            foreach (UnityEngine.Transform child in transform)
            {
                SetLayerToChildren(layer, child);
            }
        }
    }
}
