using System;
using System.Threading.Tasks;
using LingoBank.Core.Commands;
using LingoBank.Database.Entities;
using Microsoft.AspNetCore.Identity;

namespace LingoBank.Core.CommandHandlers
{
    public sealed class EditUserCommandHandler : IRuntimeCommandHandler<EditUserCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public EditUserCommandHandler(UserManager<ApplicationUser> userManager) => _userManager = userManager;


        public async Task<RuntimeCommandResult> ExecuteAsync(EditUserCommand command)
        {
            var appUser = await _userManager.FindByIdAsync(command.User.Id);
            if (appUser == null)
            {
                return new RuntimeCommandResult
                {
                    IsError = true,
                    Message = $"User with {command.User.Id} does not exist!"
                };
            }

            appUser.Email = command.User.EmailAddress;
            appUser.UserName = command.User.UserName;

            IdentityResult result = await _userManager.UpdateAsync(appUser);

            return new RuntimeCommandResult
            {
                IsError = result.Succeeded,
                Message = result.Succeeded ? "User Updated." : "Database error occurred whilst updating user."
            };
        }
    }
}