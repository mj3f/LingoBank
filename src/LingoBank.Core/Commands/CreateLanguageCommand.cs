using LingoBank.Core.Dtos;

namespace LingoBank.Core.Commands
{
    public sealed class CreateLanguageCommand : IRuntimeCommand
    {
        public LanguageDto LanguageDto { get; set; }

        public bool Validate() => LanguageDto != null;

        public bool HasExecuted { get; set; }
    }
}