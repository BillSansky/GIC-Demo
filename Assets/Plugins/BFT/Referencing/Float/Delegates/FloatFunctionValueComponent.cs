namespace BFT
{
    public class FloatFunctionValueComponent : FunctionValueComponent<float>
    {
        public FloatFunction FloatFunction;
        public override Function<float> Function => FloatFunction;
    }
}
