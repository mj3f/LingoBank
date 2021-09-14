using LingoBank.Core.Dtos;

namespace LingoBank.Core.Queries
{
    public sealed class GetLanguageByIdQuery : IRuntimeQuery<LanguageDto>
    {
        public string Id { get; set; }
        public bool Validate() => !string.IsNullOrEmpty(Id);
    }
}