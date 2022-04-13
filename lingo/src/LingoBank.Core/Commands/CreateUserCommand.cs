using LingoBank.Core.Dtos;

namespace LingoBank.Core.Commands
{
    public class CreateUserCommand : IRuntimeCommand
    {
        public UserWithPasswordDto UserWithPassword { get; set; }

        public bool Validate()
        {
            return UserWithPassword != null &&
                   !string.IsNullOrEmpty(UserWithPassword.Password) &&
                   !string.IsNullOrEmpty(UserWithPassword.EmailAddress) &&
                   !string.IsNullOrEmpty(UserWithPassword.UserName);
        }

        public bool HasExecuted { get; set; }
    }
}