namespace LingoBank.Core
{
    public sealed class RuntimeCommandResult
    {
        public string Message { get; set; }
        
        /// <summary>
        /// Determines if the message returned by the command is an error message.
        /// </summary>
        public bool IsError { get; set; }
    }
}