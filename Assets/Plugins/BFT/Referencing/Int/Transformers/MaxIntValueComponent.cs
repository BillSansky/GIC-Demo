using System.Linq;
using UnityEngine;

namespace BFT
{
    public class MaxIntValueComponent : MonoBehaviour, IValue<int>
    {
        public IntValue[] IntValues;

        public int IntValue
        {
            get { return IntValues.Max(f => f.Value); }
        }

        public int Value => IntValue;
    }
}
