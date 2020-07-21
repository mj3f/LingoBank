namespace LingoBank.Core.Dtos
{
    public sealed class PhraseDto
    {
        public string Id { get; set; }
        
        public string SourceLanguage { get; set; }
        
        public string TargetLanguage { get; set; }
        
        public string Text { get; set; }
        
        public string Translation { get; set; }
        
        public string Description { get; set; }
        
        public int Category { get; set; }
    }
}