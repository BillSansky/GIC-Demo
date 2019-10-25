namespace BFT
{
    public class StringFunctionValueComponent : FunctionValueComponent<string>
    {
        public StringFunction StringFunction;
        public override Function<string> Function => StringFunction;
    }
}
