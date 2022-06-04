using LingoBank.Core.Dtos;

namespace LingoBank.Core.Queries
{
    /// <summary>
    /// Returns a language with all of its phrases.
    /// </summary>
    public sealed class GetLanguageByIdQuery : IRuntimeQuery<LanguageDto?>
    {
        public string Id { get; set; }
        public bool Validate() => !string.IsNullOrEmpty(Id);
    }
}