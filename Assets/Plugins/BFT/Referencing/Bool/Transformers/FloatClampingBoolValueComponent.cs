using UnityEngine;

namespace BFT
{
    public class FloatClampingBoolValueComponent : MonoBehaviour, IValue<bool>
    {
        public FloatValue FloatValue;
        public float MinForTrue, MaxForTrue;

        public bool Value => MathExt.IsClampedInclusive(FloatValue.Value, MinForTrue, MaxForTrue);
    }
}
