using System;
using System.Threading.Tasks;
using LingoBank.Core.Commands;
using LingoBank.Database.Entities;
using Microsoft.AspNetCore.Identity;

namespace LingoBank.Core.CommandHandlers
{
    public class CreateUserCommandHandler : IRuntimeCommandHandler<CreateUserCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateUserCommandHandler(UserManager<ApplicationUser> userManager) => _userManager = userManager;


        public async Task<RuntimeCommandResult> ExecuteAsync(CreateUserCommand command)
        {
            var appUser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = command.CreateUser.UserName,
                Email = command.CreateUser.EmailAddress,
                Role = command.Role
            };
            IdentityResult result = await _userManager.CreateAsync(appUser, command.CreateUser.Password);

            return new RuntimeCommandResult(result.Succeeded, result.Errors.ToString());
        }
    }
}