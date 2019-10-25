namespace BFT
{
    public class IntValueComponent : ValueComponent<int, IntValue>, IValue<float>, IValue<string>
    {
        float IValue<float>.Value => Value;
        string IValue<string>.Value => Value.ToString();
    }
}
