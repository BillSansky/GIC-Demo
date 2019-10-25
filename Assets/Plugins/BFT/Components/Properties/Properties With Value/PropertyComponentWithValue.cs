namespace BFT
{
    public abstract class PropertyComponentWithValue<T, T1> : PropertyComponent, IValue<T> where T1 : GenericValue<T>
    {
        public T1 DataValue;

        public IValue<T> ComponentData
        {
            get
            {
                UnityEngine.Debug.Assert(DataValue is IValue<T>,
                    $"The component {DataValue} is not of the required type, check your code / value", this);
                return (IValue<T>) DataValue;
            }
        }

        public override object Data => ComponentData;
        public T Value => DataValue.Value;
    }
}
