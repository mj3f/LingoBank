namespace LingoBank.Core.Queries;

/// <summary>
/// A query that returns data from the database in paginated form.
/// </summary>
public interface IPaginatedQuery
{
    /// <summary>
    /// The page number for which to fetch a subset of data for.
    /// </summary>
    int Page { get; init; }
}