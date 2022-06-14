using LingoBank.Core.Dtos;

namespace LingoBank.Core.Queries;

/// <summary>
/// Returns a paginated list of all phrases in the system. This query is accessible from the PhrasesController, and can only be
/// called by users with the Admin role.
/// </summary>
public class GetPhrasesQuery : IRuntimeQuery<Paged<PhraseDto>>, IPaginatedQuery
{
    /// <summary>
    /// The page to fetch data for.
    /// </summary>
    public int Page { get; init; }

    public bool Validate() => Page > 0;
}