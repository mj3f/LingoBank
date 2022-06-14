using LingoBank.Core.Dtos;

namespace LingoBank.Core.Commands
{
    public class CreateUserCommand : IRuntimeCommand
    {
        public CreateUserDto CreateUser { get; init; } = null!;
        public string Role { get; init; } = null!;

        public bool Validate()
        {
            return CreateUser != null &&
                   !string.IsNullOrEmpty(Role) &&
                   !string.IsNullOrEmpty(CreateUser.Password) &&
                   !string.IsNullOrEmpty(CreateUser.EmailAddress) &&
                   !string.IsNullOrEmpty(CreateUser.UserName);
        }

        public bool HasExecuted { get; set; }
    }
}