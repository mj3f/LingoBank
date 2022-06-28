using LingoBank.Core.Enums;

namespace LingoBank.Core.Dtos
{
    public sealed record PhraseDto
    {
        /// <summary>
        /// unique identifier
        /// </summary>
        public string Id { get; init; } = default!;
        
        /// <summary>
        /// Language that the phrase belongs to.
        /// </summary>
        public string LanguageId { get; init; } = default!;
        
        /// <summary>
        /// The source language of the phrase.
        /// </summary>
        public string SourceLanguage { get; init; } = default!;
        
        /// <summary>
        /// The language that the phrase is to be translated to.
        /// </summary>
        public string TargetLanguage { get; init; } = default!;
        
        /// <summary>
        /// The phrases text, in the defined source language.
        /// </summary>
        public string Text { get; init; } = default!;
        
        /// <summary>
        /// The target language translation of text.
        /// </summary>
        public string Translation { get; init; } = default!;
        
        /// <summary>
        /// Description of the phrase.
        /// </summary>
        public string Description { get; init; } = default!;
        
        /// <summary>
        /// The category for which the phrase belongs to.
        /// </summary>
        public Category Category { get; init; }
    }
}