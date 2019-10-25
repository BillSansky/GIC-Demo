using Sirenix.OdinInspector;

namespace BFT
{
    public class BoolToSpriteComponent : SerializedMonoBehaviour, IValue<UnityEngine.Sprite>
    {
        [BoxGroup("Condition")] public IValue<bool> Condition;

        [BoxGroup("Sprites")] public UnityEngine.Sprite SpriteOnFalse;

        [BoxGroup("Sprites")] public UnityEngine.Sprite SpriteOnTrue;

        public UnityEngine.Sprite Value => Condition.Value ? SpriteOnTrue : SpriteOnFalse;
    }
}
