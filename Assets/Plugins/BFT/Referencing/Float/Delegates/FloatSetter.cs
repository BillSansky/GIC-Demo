using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class FloatSetter : SerializedMonoBehaviour
    {
        [SerializeField] private Func<float> Get;

        [SerializeField] private Action<float> Set;

        public void Invoke()
        {
            Set.Invoke(Get.Invoke());
        }
    }
}
