using LingoBank.Core.Dtos;

namespace LingoBank.Core.Queries;

public class GetLanguagesByUserIdQuery : IRuntimeQuery<Paged<LanguageDto>>, IPaginatedQuery
{
    /// <summary>
    /// User id to search for languages.
    /// </summary>
    public string UserId { get; init; }

    /// <summary>
    /// Page to search data for.
    /// </summary>
    public int Page { get; init; } = 1;

    public bool Validate() => !string.IsNullOrEmpty(UserId);
}