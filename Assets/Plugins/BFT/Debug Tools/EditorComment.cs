#if UNITY_EDITOR

using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    [AddComponentMenu("Debug/Editor Comment")]
    public class EditorComment : MonoBehaviour
    {
        [Tooltip("A comment that wont make it to compilation")] [TextArea(0, 10), HideLabel]
        public string Note;
    }
}

#endif
