namespace BFT
{
    /// <summary>
    ///     Use this interface when you want the user to be able to read or write the value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="T1"></typeparam>
    public interface IVariable<T> : IValue<T>
    {
        new T Value { get; set; }
    }
}
