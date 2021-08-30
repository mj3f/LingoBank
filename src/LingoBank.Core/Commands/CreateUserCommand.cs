using System;
using LingoBank.Core.Dtos;
using Microsoft.AspNetCore.Identity;

namespace LingoBank.Core.Commands
{
    public class CreateUserCommand : IRuntimeCommand
    {
        public UserWithPasswordDto UserWithPassword { get; set; }
        
        public Action<IdentityResult> ResultCallback { get; set; }
        
        public bool Validate()
        {
            if (UserWithPassword != null &&
                !string.IsNullOrEmpty(UserWithPassword.Password) &&
                !string.IsNullOrEmpty(UserWithPassword.EmailAddress) &&
                !string.IsNullOrEmpty(UserWithPassword.UserName))
            {
                return true;
            }

            return false;
        }

        public bool HasExecuted { get; set; }
    }
}