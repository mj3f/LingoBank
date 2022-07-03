using System;
using System.Threading.Tasks;
using LingoBank.Core.Commands;
using LingoBank.Core.Exceptions;
using LingoBank.Core.Utils;
using LingoBank.Database.Entities;
using Microsoft.AspNetCore.Identity;

namespace LingoBank.Core.CommandHandlers
{
    public class CreateUserCommandHandler : IRuntimeCommandHandler<CreateUserCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateUserCommandHandler(UserManager<ApplicationUser> userManager) => _userManager = userManager;


        public async Task ExecuteAsync(CreateUserCommand command)
        {
            var appUser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = command.CreateUser.UserName,
                Email = command.CreateUser.EmailAddress,
                Role = command.Role
            };
            IdentityResult result = await _userManager.CreateAsync(appUser, command.CreateUser.Password);

            if (!result.Succeeded)
            {
                string errorMessage = IdentityResultErrorsFormatter.GetFormattedErrorMessage(result.Errors);
                throw new RuntimeException(errorMessage);
            }
        }
    }
}