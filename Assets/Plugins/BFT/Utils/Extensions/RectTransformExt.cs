using UnityEngine;

namespace BFT
{
    public static class RectTransformExt
    {
        public static void RectCopy(this RectTransform targetTransform, RectTransform srcTransform, bool copySize = true)
        {
            targetTransform.pivot = srcTransform.pivot;
            targetTransform.anchoredPosition = srcTransform.anchoredPosition;
            targetTransform.anchorMin = srcTransform.anchorMin;
            targetTransform.anchorMax = srcTransform.anchorMax;

            targetTransform.position = srcTransform.position;

            if (copySize)
            {
                targetTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, srcTransform.rect.width);
                targetTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, srcTransform.rect.height);
            }

            targetTransform.rotation = srcTransform.rotation;
            if (copySize)
            {
                Vector3 srcScale = srcTransform.lossyScale;
                Vector3 targetScale = targetTransform.parent != null ? targetTransform.parent.lossyScale : Vector3.one;
                targetTransform.localScale = new Vector3(srcScale.x / targetScale.x, srcScale.y / targetScale.y,
                    srcScale.z / targetScale.z);
            }
        }
    }
}
