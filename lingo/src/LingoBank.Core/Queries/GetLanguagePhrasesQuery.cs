using LingoBank.Core.Dtos;

namespace LingoBank.Core.Queries;

public class GetLanguagePhrasesQuery : IRuntimeQuery<Paged<PhraseDto>?>, IPaginatedQuery
{
    public string LanguageId { get; init; }
    
    public int Page { get; init; }
    public bool Validate() => !string.IsNullOrEmpty(LanguageId) && Page > 0;
}