namespace BFT
{
    /// <summary>
    ///     Simply owns an ID, without any mention of a dictionary or default value, and can change the value.
    /// </summary>
    public interface IIDEntry : IIDValue
    {
        new int ID { get; set; }
    }
}
