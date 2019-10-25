namespace BFT
{
    public class BoolFunctionValueComponent : FunctionValueComponent<bool>
    {
        public BoolFunction BoolFunction;
        public override Function<bool> Function => BoolFunction;
    }
}
