using LingoBank.Core.Dtos;

namespace LingoBank.Core.Commands
{
    public sealed class EditPhraseCommand : IRuntimeCommand
    {
        public PhraseDto Phrase { get; set; }

        public bool Validate()
        {
            if (string.IsNullOrEmpty(Phrase?.LanguageId) ||
                string.IsNullOrEmpty(Phrase.SourceLanguage) ||
                string.IsNullOrEmpty(Phrase.TargetLanguage) ||
                string.IsNullOrEmpty(Phrase.Text) ||
                string.IsNullOrEmpty(Phrase.Translation))
            {
                return false;
            }

            return true;
        }
        
        public bool HasExecuted { get; set; }
    }
}