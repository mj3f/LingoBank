using LingoBank.Core.Dtos;

namespace LingoBank.Core.Queries;

/// <summary>
/// Returns a paginated list of all languages in the database.
/// Only called by administrators, and really only for the purposes of debugging.
/// </summary>
public class GetLanguagesQuery : IRuntimeQuery<Paged<LanguageDto>?>, IPaginatedQuery
{
    public int Page { get; init; }
    public bool Validate() => true;
}