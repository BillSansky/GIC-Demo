using System;
using UnityEngine;

namespace BFT
{
    [Serializable]
    public class AdvancedGradient : IValue<UnityEngine.Gradient>
    {
        [SerializeField] private UnityEngine.Gradient gradient;

        public UnityEngine.Gradient Value => gradient;
    }
}
