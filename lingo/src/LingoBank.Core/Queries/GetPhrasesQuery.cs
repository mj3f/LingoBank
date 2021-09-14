using System.Collections.Generic;
using LingoBank.Core.Dtos;

namespace LingoBank.Core.Queries
{
    public sealed class GetPhrasesQuery : IRuntimeQuery<List<PhraseDto>>
    {
        public string LanguageId { get; set; }
        public bool Validate() => !string.IsNullOrEmpty(LanguageId);
    }
}