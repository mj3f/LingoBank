using System.Threading.Tasks;
using LingoBank.Core.Commands;
using LingoBank.Database.Entities;
using Microsoft.AspNetCore.Identity;

namespace LingoBank.Core.CommandHandlers
{
    public sealed class DeleteUserCommandHandler : IRuntimeCommandHandler<DeleteUserCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteUserCommandHandler(UserManager<ApplicationUser> userManager) => _userManager = userManager;


        public async Task<RuntimeCommandResult> ExecuteAsync(DeleteUserCommand command)
        {
            var appUser = await _userManager.FindByIdAsync(command.Id);
            if (appUser == null)
            {
                return new RuntimeCommandResult
                {
                    IsError = true,
                    Message = $"User with {command.Id} does not exist!"
                };
            }

            IdentityResult result = await _userManager.DeleteAsync(appUser);

            return new RuntimeCommandResult
            {
                IsError = result.Succeeded,
                Message = result.Succeeded ? "User Deleted." : "Database error occurred whilst attempting to delete user. Action failed."
            };
        }
    }
}