using System.Collections.Generic;

namespace LingoBank.Core.Dtos
{
    public sealed record LanguageDto
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        public string Id { get; init; } = default!;
        
        /// <summary>
        /// The name of the language.
        /// </summary>
        public string Name { get; init; } = default!;

        /// <summary>
        /// Optional description of the users created language.
        /// </summary>
        public string Description { get; init; } = default!;

        /// <summary>
        /// ISO 3166 2 character country code, used to support showing a flag next to the language.
        /// I.e. For the English language, we could show either the British or American flags. 
        /// </summary>
        public string Code { get; init; } = default!;
        
        /// <summary>
        /// The user that this language belongs to.
        /// </summary>
        public string UserId { get; init; } = default!;

        /// <summary>
        /// List of phrases that make up the language.
        /// </summary>
        public ICollection<PhraseDto> Phrases { get; init; } = default!;
    }
}