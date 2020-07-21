using System.Collections.Generic;

namespace LingoBank.Core.Dtos
{
    public sealed class LanguageDto
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

        public ICollection<PhraseDto> Phrases { get; set; }
    }
}