namespace BFT
{
    /// <summary>
    ///     a value that references a dictionary
    /// </summary>
    /// <typeparam name="T"> the type of data stored</typeparam>
    /// <typeparam name="T1">the type of dictionary entry</typeparam>
    /// <typeparam name="T2">the type of dictionary</typeparam>
    public class EntryDictionaryValue<T, T1, T2> : GenericDifferentialValue<IEntryDictionary<T>, T2>
        where T1 : class, IDictionaryEntry<T> where T2 : EntryDictionary<T, T1>
    {
    }
}
