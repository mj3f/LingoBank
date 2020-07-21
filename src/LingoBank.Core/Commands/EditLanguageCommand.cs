using LingoBank.Core.Dtos;

namespace LingoBank.Core.Commands
{
    public sealed class EditLanguageCommand : IRuntimeCommand
    {
        public string Id { get; set; }
        public LanguageDto Language { get; set; }

        public bool Validate() => !string.IsNullOrEmpty(Id) && Language != null && !string.IsNullOrEmpty(Language.Name);

        public bool HasExecuted { get; set; }
    }
}