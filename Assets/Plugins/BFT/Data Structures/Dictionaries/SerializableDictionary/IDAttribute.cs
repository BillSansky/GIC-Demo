namespace BFT
{
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class IDAttribute : System.Attribute
    {
        private string _id;

        /// <summary>
        ///     Serializable field name for the property id
        /// </summary>
        /// <param name="id">Field name</param>
        public IDAttribute(string id)
        {
            _id = id;
        }

        public string Id => _id;
    }
}
