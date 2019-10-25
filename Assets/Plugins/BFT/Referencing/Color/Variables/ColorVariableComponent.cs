namespace BFT
{
    public class ColorVariableComponent : VariableComponent<UnityEngine.Color>
    {
        public ColorVariable ColorVariable;
        public override GenericVariable<UnityEngine.Color> Variable => ColorVariable;
    }
}
