namespace LingoBank.Core.Commands
{
    public class DeleteUserCommand : IRuntimeCommand
    {
        public string Id { get; set; }

        public bool Validate() => !string.IsNullOrEmpty(Id);
        
        public bool HasExecuted { get; set; }
    }
}