namespace LingoBank.Core.Commands
{
    public interface IRuntimeCommand
    {
        /// <summary>
        /// Checks whether the command is valid for execution or not.
        /// </summary>
        /// <returns>bool</returns>
        bool Validate();
        
        /// <summary>
        /// Property to determine whether the command has been executed or not.
        /// </summary>
        bool HasExecuted { get; set; }
    }
}