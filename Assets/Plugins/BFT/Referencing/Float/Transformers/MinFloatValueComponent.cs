using System.Linq;
using UnityEngine;

namespace BFT
{
    public class MinFloatValueComponent : MonoBehaviour, IValue<float>
    {
        public FloatValue[] FloatValues;

        public float FloatValue
        {
            get { return FloatValues.Min(f => f.Value); }
        }

        public float Value => FloatValue;
    }
}
