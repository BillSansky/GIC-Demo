using UnityEngine;

namespace BFT
{
    public static class LayerMaskExt
    {
        public static bool IsInLayerMask(this LayerMask mask, GameObject obj)
        {
            return ((mask.value & (1 << obj.layer)) > 0);
        }
    }
}
