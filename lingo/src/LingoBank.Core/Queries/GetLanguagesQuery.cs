using LingoBank.Core.Dtos;

namespace LingoBank.Core.Queries;

/// <summary>
/// Returns a paginated list of all languages in the database.
/// Only called by administrators, and really only for the purposes of debugging.
/// </summary>
public class GetLanguagesQuery : IRuntimeQuery<Paged<LanguageDto>>, IPaginatedQuery
{
    /// <summary>
    /// The page to fetch data for.
    /// </summary>
    public int Page { get; init; } = 1;
    
    public bool Validate() => true;
}