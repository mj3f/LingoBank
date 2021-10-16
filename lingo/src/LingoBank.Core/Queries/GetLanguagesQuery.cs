using System.Collections.Generic;
using LingoBank.Core.Dtos;

namespace LingoBank.Core.Queries
{
    public sealed class GetLanguagesQuery : IRuntimeQuery<List<LanguageDto>>
    {
        public string UserId { get; set; }
        public bool Validate() => !string.IsNullOrEmpty(UserId);
    }
}