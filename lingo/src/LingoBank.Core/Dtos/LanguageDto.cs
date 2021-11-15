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
        /// Optional description of the users created language.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// ISO 3166 2 character country code, used to support showing a flag next to the language.
        /// I.e. For the English language, we could show either the British or American flags. 
        /// </summary>
        public string Code { get; set; }
        
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