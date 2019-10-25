using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    [AddComponentMenu("Transform/Utils/Scale Randomizer")]
    public class TransformScaleRandomizer : MonoBehaviour
    {
        public FloatValue MaxSize;
        public FloatValue MinSize;

        private bool onStart = false;

        // Use this for initialization
        void Start()
        {
            if (onStart)
                RandomScale();
        }

        [Button]
        private void RandomScale()
        {
            transform.localScale = transform.localScale * Random.Range(MinSize.Value, MaxSize.Value);
        }
    }
}
