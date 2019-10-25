using System;
using Random = UnityEngine.Random;

namespace BFT
{
    [Serializable]
    public class RandomFloatValue : IValue<float>
    {
        public float MaxValue = 1;
        public float MinValue = 0;


        public float Value => Random.Range(MinValue, MaxValue);
    }
}
