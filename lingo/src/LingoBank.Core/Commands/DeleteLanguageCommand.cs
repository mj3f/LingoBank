namespace LingoBank.Core.Commands;

public sealed class DeleteLanguageCommand : IRuntimeCommand
{
    public string Id { get; init; }
    
    public bool Validate() => !string.IsNullOrEmpty(Id);

    public bool HasExecuted { get; set; }
}