using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class MaxFloatValueComponent : MonoBehaviour,
        IValue<float>
    {
        public FloatValue[] FloatGivers;

        public float FloatValue
        {
            get { return FloatGivers.Max(f => f.Value); }
        }

        [ShowInInspector] public float Value => FloatValue;
    }
}
