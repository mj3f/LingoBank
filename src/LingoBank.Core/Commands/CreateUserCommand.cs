using LingoBank.Core.Dtos;

namespace LingoBank.Core.Commands
{
    public class CreateUserCommand : IRuntimeCommand
    {
        public CreateUserDto User { get; set; }
        
        public bool Validate()
        {
            if (User != null &&
                !string.IsNullOrEmpty(User.Password) &&
                !string.IsNullOrEmpty(User.EmailAddress) &&
                !string.IsNullOrEmpty(User.UserName))
            {
                return true;
            }

            return false;
        }

        public bool HasExecuted { get; set; }
    }
}