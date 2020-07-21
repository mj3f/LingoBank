namespace LingoBank.Core
{
    public interface IRuntimeQuery<TResult>
    {
        /// <summary>
        /// Checks whether the query is valid for execution or not.
        /// If validation is not required for a query, just return true.
        /// </summary>
        /// <returns>bool</returns>
        bool Validate();
    }
}