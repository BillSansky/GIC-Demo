using UnityEngine;

namespace BFT
{
    public class RandomBoolValueComponent : MonoBehaviour, IValue<bool>
    {
        public FloatValue ChancesForTrue;

        public bool Value => Random.value <= ChancesForTrue.Value;
    }
}
