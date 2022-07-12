using System.Threading.Tasks;
using LingoBank.Core.Exceptions;
using LingoBank.Core.Queries;
using LingoBank.Database.Contexts;
using LingoBank.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LingoBank.Core.QueryHandlers;

public sealed class GetPasswordResetTokenQueryHandler : IRuntimeQueryHandler<GetPasswordResetTokenQuery, string>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly LingoContext _context;

    public GetPasswordResetTokenQueryHandler(
        UserManager<ApplicationUser> userManager,
        LingoContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<string> ExecuteAsync(GetPasswordResetTokenQuery query)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == query.UserId);

        if (user is null)
        {
            throw new RuntimeException($"No user exists with id {query.UserId}");
        }

        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }
}