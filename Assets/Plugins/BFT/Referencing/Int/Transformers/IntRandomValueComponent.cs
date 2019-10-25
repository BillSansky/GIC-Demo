using UnityEngine;

namespace BFT
{
    public class IntRandomValueComponent : MonoBehaviour, IValue<int>
    {
        [SerializeField] private IntValue from;

        [SerializeField] private IntValue to;

        public int Value => Random.Range(from.Value, to.Value);
    }
}
