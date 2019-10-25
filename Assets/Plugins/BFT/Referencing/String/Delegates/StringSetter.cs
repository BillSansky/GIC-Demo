using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class StringSetter : SerializedMonoBehaviour
    {
        [SerializeField] private Func<string> Get;

        [SerializeField] private Action<string> Set;

        public void Invoke()
        {
            Set.Invoke(Get.Invoke());
        }
    }
}
