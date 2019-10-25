using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    class TransformOnEnablePositionSetter : MonoBehaviour
    {
        public TransformValue ToCopy;

        public void OnEnable()
        {
            Copy();
        }

        [Button(ButtonSizes.Medium)]
        private void Copy()
        {
            transform.position = ToCopy.position;
        }
    }
}
