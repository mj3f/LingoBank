using System.Collections.Generic;
using LingoBank.Core.Dtos;

namespace LingoBank.Core.Queries
{
    public sealed class GetLanguagesQuery : IRuntimeQuery<List<LanguageDto>>
    {
        public bool Validate() => true;
    }
}