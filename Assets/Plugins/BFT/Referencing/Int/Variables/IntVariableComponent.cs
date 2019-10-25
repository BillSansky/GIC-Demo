namespace BFT
{
    public class IntVariableComponent : VariableComponent<int>, IValue<float>
    {
        public IntVariable IntVariable;

        public override GenericVariable<int> Variable => IntVariable;
        float IValue<float>.Value => Value;

        public void Increment()
        {
            Value++;
        }

        public void Decrement()
        {
            Value--;
        }

        public void Add(int amount)
        {
            Value += amount;
        }
    }
}
