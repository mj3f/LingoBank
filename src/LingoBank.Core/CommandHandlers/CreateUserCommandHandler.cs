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


        public async Task ExecuteAsync(CreateUserCommand command)
        {
            var appUser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = command.UserWithPassword.UserName,
                Email = command.UserWithPassword.EmailAddress,
                Role = command.UserWithPassword.Role ?? "User"
            };
            IdentityResult result = await _userManager.CreateAsync(appUser, command.UserWithPassword.Password);
        }
    }
}