namespace LingoBank.Core.Dtos
{
    public sealed class PhraseDto
    {
        /// <summary>
        /// unique identifier
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Language that the phrase belongs to.
        /// </summary>
        public string LanguageId { get; set; }
        
        /// <summary>
        /// The source language of the phrase.
        /// </summary>
        public string SourceLanguage { get; set; }
        
        /// <summary>
        /// The language that the phrase is to be translated to.
        /// </summary>
        public string TargetLanguage { get; set; }
        
        /// <summary>
        /// The phrases text, in the defined source language.
        /// </summary>
        public string Text { get; set; }
        
        /// <summary>
        /// The target language translation of text.
        /// </summary>
        public string Translation { get; set; }
        
        /// <summary>
        /// Description of the phrase.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// The category for which the phrase belongs to.
        /// </summary>
        public int Category { get; set; }
    }
}