using System;
using System.Collections.Generic;
using LingoBank.Core.Dtos;

namespace LingoBank.Core.Queries
{
    [Obsolete("This shouldn't be needed if the get user by id query returns a list of the users languages already.")]
    public sealed class GetLanguagesQuery : IRuntimeQuery<List<LanguageDto>>
    {
        public string UserId { get; set; }
        public bool Validate() => !string.IsNullOrEmpty(UserId);
    }
}