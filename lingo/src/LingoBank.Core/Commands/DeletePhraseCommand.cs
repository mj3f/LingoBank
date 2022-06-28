namespace LingoBank.Core.Commands;

/// <summary>
/// Removes a phrase from the database.
/// </summary>
public sealed class DeletePhraseCommand : IRuntimeCommand
{
    public string Id { get; init; }
    public bool Validate() => !string.IsNullOrEmpty(Id);

    public bool HasExecuted { get; set; }
}