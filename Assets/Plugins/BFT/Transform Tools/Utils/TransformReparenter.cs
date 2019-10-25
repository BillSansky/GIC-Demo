using UnityEngine;

namespace BFT
{
    public class TransformReparenter : MonoBehaviour
    {
        public TransformValue NewParent;
        public Transform ToReparent;

        void Reset()
        {
            ToReparent = transform;
        }

        public void ReParent()
        {
            ToReparent.SetParent(NewParent.Value);
        }
    }
}
