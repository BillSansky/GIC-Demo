namespace BFT
{
    public class FloatVariableComponent : VariableComponent<float>
    {
        public FloatVariable FloatVariable;
        public override GenericVariable<float> Variable => FloatVariable;
    }
}
