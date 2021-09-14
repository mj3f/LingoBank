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


        public async Task ExecuteAsync(DeleteUserCommand command)
        {
            var appUser = await _userManager.FindByIdAsync(command.Id);

            IdentityResult result = await _userManager.DeleteAsync(appUser);

          
        }
    }
}