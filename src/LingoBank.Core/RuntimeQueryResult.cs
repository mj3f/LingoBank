namespace LingoBank.Core
{
    public sealed class RuntimeQueryResult<T> where T : class // TODO: Implement this in QueryHandlers.
    {
        /// <summary>
        /// The data to be returned by the query.
        /// </summary>
        public T Data { get; set; }
        
        /// <summary>
        /// If not empty, then an error occurred whilst fetching data, show this to the user.
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}