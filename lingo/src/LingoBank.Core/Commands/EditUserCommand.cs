using LingoBank.Core.Dtos;

namespace LingoBank.Core.Commands
{
    public sealed class EditUserCommand : IRuntimeCommand
    {
        public UserDto User { get; set; }
        
        public bool Validate()
        {
            return User != null &&
                   !string.IsNullOrEmpty(User.EmailAddress) &&
                   !string.IsNullOrEmpty(User.UserName);
        }

        public bool HasExecuted { get; set; }
    }
}