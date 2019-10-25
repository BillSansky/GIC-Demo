namespace BFT
{
    /// <summary>
    ///     a data held into a dictionary with a name reference,
    ///     and that holds a reference to its ID in the dictionary
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    public interface IDictionaryEntry<T1> : IJSonDataConverter, IIDValue
    {
        T1 Data { get; set; }
        string NameID { get; set; }

        void RefreshDictionaryKey(int newID);
    }
}
