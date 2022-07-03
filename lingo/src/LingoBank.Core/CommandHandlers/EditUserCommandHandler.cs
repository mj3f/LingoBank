using System;
using System.Threading.Tasks;
using LingoBank.Core.Commands;
using LingoBank.Core.Exceptions;
using LingoBank.Core.Utils;
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
                string errorMessage = IdentityResultErrorsFormatter.GetFormattedErrorMessage(result.Errors);
                throw new RuntimeException(errorMessage);
            }
        }
    }
}