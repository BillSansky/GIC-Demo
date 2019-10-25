namespace BFT
{
    public class WeightedObjectVariableList : WeightedListAsset<IValue<object>>, IVariable<object>
    {
        public new object Value
        {
            get => GetRandomElement().Value;
            set =>
                WeightedValues.Add(new WeightedValue()
                {
                    Value = new GenericVariable<object>(value),
                    Weight = 1
                });
        }
    }
}
