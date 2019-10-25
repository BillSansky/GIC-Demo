using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class BoolToColorComponent : MonoBehaviour, IValue<UnityEngine.Color>
    {
        [BoxGroup("Sprites")] public ColorValue ColorOnFalse;

        [BoxGroup("Sprites")] public ColorValue ColorOnTrue;

        [BoxGroup("Condition")] public IValue<bool> Condition;

        public UnityEngine.Color Value => Condition.Value ? ColorOnTrue.Value : ColorOnFalse.Value;
    }
}
