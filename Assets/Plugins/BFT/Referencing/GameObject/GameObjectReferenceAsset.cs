using UnityEngine;

namespace BFT
{
    public class GameObjectReferenceAsset : ScriptableObject, IValue<UnityEngine.GameObject>
    {
        public UnityEngine.GameObject Reference;
        public UnityEngine.GameObject Value => Reference;

        public void SetReference(UnityEngine.GameObject go)
        {
            Reference = go;
        }
    }
}
