namespace BFT
{
    public class SpriteFunctionValueComponent : FunctionValueComponent<UnityEngine.Sprite>
    {
        public SpriteFunction SpriteFunction;
        public override Function<UnityEngine.Sprite> Function => SpriteFunction;
    }
}
