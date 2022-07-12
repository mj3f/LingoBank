using System.Threading.Tasks;
using LingoBank.Core.Commands;
using LingoBank.Core.Exceptions;
using LingoBank.Core.Utils;
using LingoBank.Database.Contexts;
using LingoBank.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LingoBank.Core.CommandHandlers;

public sealed class ResetUserPasswordCommandHandler : IRuntimeCommandHandler<ResetUserPasswordCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly LingoContext _context;

    public ResetUserPasswordCommandHandler(
        UserManager<ApplicationUser> userManager,
        LingoContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task ExecuteAsync(ResetUserPasswordCommand command)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == command.UserId);

        if (user is null)
        {
            throw new RuntimeException($"No user exists with id {command.UserId}");
        }

        IdentityResult result = await _userManager.ResetPasswordAsync(user, command.ResetToken, command.NewPassword);

        if (!result.Succeeded)
        {
            string errorMessage = IdentityResultErrorsFormatter.GetFormattedErrorMessage(result.Errors);
            throw new RuntimeException(errorMessage);
        }
    }
}