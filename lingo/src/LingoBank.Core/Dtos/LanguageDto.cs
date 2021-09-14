using System.Collections.Generic;

namespace LingoBank.Core.Dtos
{
    public sealed class LanguageDto
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// The name of the language.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The user that this language belongs to.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// List of phrases that make up the language.
        /// </summary>
        public ICollection<PhraseDto> Phrases { get; set; }
    }
}