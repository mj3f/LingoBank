using LingoBank.Core.Dtos;

namespace LingoBank.Core.Commands
{
    public sealed class CreateLanguageCommand : IRuntimeCommand
    {
        public LanguageDto Language { get; set; }

        public bool Validate() => Language != null && !string.IsNullOrEmpty(Language.Name);

        public bool HasExecuted { get; set; }
    }
}