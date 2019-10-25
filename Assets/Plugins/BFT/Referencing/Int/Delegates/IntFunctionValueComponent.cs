namespace BFT
{
    public class IntFunctionValueComponent : FunctionValueComponent<int>
    {
        public IntFunction IntFunction;
        public override Function<int> Function => IntFunction;
    }
}
