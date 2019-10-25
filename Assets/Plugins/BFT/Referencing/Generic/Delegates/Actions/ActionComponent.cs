using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class ActionComponent : MonoBehaviour
    {
        public Action Action;

        [Button(ButtonSizes.Medium)]
        public void Invoke()
        {
            Action.Act();
        }
    }
}
