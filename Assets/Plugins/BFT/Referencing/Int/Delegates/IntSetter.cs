using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class IntSetter : SerializedMonoBehaviour
    {
        [SerializeField] private IntFunction Get;

        [SerializeField] private IntAction Set;

        public void Invoke()
        {
            Set.Invoke(Get.Value);
        }
    }
}
