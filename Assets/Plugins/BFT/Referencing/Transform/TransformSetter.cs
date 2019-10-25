using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class TransformSetter : SerializedMonoBehaviour
    {
        [SerializeField] private Func<UnityEngine.Transform> Get;

        [SerializeField] private Action<UnityEngine.Transform> Set;

        public void Invoke()
        {
            Set.Invoke(Get.Invoke());
        }
    }
}
