using System.Linq;
using UnityEngine;

namespace BFT
{
    public class MinIntValueComponent : MonoBehaviour, IValue<int>
    {
        public IntValue[] IntValues;

        public int IntValue
        {
            get { return IntValues.Min(f => f.Value); }
        }

        public int Value => IntValue;
    }
}
