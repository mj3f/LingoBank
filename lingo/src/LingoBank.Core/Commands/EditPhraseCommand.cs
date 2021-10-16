using LingoBank.Core.Dtos;

namespace LingoBank.Core.Commands
{
    public sealed class EditPhraseCommand : IRuntimeCommand
    {
        public string Id { get; set; }
        public PhraseDto Phrase { get; set; }

        public bool Validate()
        {
            return Phrase != null &&
                   !string.IsNullOrEmpty(Phrase.LanguageId) &&
                   !string.IsNullOrEmpty(Phrase.SourceLanguage) &&
                   !string.IsNullOrEmpty(Phrase.TargetLanguage) &&
                   !string.IsNullOrEmpty(Phrase.Text) &&
                   !string.IsNullOrEmpty(Phrase.Translation) &&
                   !string.IsNullOrEmpty(Id);
        }
        
        public bool HasExecuted { get; set; }
    }
}