using LingoBank.Core.Dtos;

namespace LingoBank.Core.Commands
{
    public sealed class CreatePhraseCommand : IRuntimeCommand
    {
        public PhraseDto Phrase { get; set; }

        public bool Validate()
        {
            return Phrase != null &&
                   !string.IsNullOrEmpty(Phrase.LanguageId) && 
                   !string.IsNullOrEmpty(Phrase.SourceLanguage) && 
                   !string.IsNullOrEmpty(Phrase.TargetLanguage) && 
                   !string.IsNullOrEmpty(Phrase.Text) && 
                   !string.IsNullOrEmpty(Phrase.Translation);
        }

        public bool HasExecuted { get; set; }
    }
}