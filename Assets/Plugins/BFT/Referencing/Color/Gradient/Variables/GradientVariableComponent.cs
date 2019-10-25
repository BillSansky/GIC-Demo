namespace BFT
{
    public class GradientVariableComponent : VariableComponent<UnityEngine.Gradient>
    {
        public GradientVariable GradientVariable;
        public override GenericVariable<UnityEngine.Gradient> Variable => GradientVariable;
    }
}
