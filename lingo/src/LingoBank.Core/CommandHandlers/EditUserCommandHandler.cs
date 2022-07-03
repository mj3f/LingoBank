using System;
using System.Threading.Tasks;
using LingoBank.Core.Commands;
using LingoBank.Core.Exceptions;
using LingoBank.Database.Entities;
using Microsoft.AspNetCore.Identity;

namespace LingoBank.Core.CommandHandlers
{
    public sealed class EditUserCommandHandler : IRuntimeCommandHandler<EditUserCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public EditUserCommandHandler(UserManager<ApplicationUser> userManager) => _userManager = userManager;


        public async Task ExecuteAsync(EditUserCommand command)
        {
            var appUser = await _userManager.FindByIdAsync(command.User.Id);
           
            appUser.Email = command.User.EmailAddress;
            appUser.UserName = command.User.UserName;

            IdentityResult result = await _userManager.UpdateAsync(appUser);

            if (!result.Succeeded)
            {
                throw new RuntimeException("Error occurred whilst editing a user."); // TODO: Unravel result.Errors (.ToString() - does it work?)
            }
        }
    }
}