namespace BFT
{
    public class SpriteVariableComponent : VariableComponent<UnityEngine.Sprite>
    {
        public SpriteVariable SpriteVariable;
        public override GenericVariable<UnityEngine.Sprite> Variable => SpriteVariable;
    }
}
