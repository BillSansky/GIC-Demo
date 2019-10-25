namespace BFT
{
    public class StringVariableComponent : VariableComponent<string>
    {
        public StringVariable StringVariable;

        public override GenericVariable<string> Variable => StringVariable;
    }
}
