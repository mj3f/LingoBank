using System;
using LingoBank.Core.Dtos;
using Microsoft.AspNetCore.Identity;

namespace LingoBank.Core.Commands
{
    public class CreateUserCommand : IRuntimeIdentityCommand
    {
        public UserWithPasswordDto UserWithPassword { get; set; }

        public Action<IdentityResult> HandleResult { get; set; }
        
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