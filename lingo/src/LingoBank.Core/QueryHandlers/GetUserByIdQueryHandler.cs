using System.Threading.Tasks;
using LingoBank.Core.Dtos;
using LingoBank.Core.Queries;
using LingoBank.Database.Contexts;
using LingoBank.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LingoBank.Core.QueryHandlers
{
    public sealed class GetUserByIdQueryHandler : IRuntimeQueryHandler<GetUserByIdQuery, UserDto?>
    {
        private readonly LingoContext _context;

        public GetUserByIdQueryHandler(LingoContext context) => _context = context;
        
        public async Task<UserDto?> ExecuteAsync(GetUserByIdQuery query)
        {
            ApplicationUser? appUser = await GetUser(query);
            
            if (appUser is null)
            {
                return null;
            }
            
            return new UserDto
            {
                Id = appUser.Id,
                EmailAddress = appUser.Email,
                Role = appUser.Role,
                UserName = appUser.UserName
            };
        }

        /// <summary>
        /// Searches for a user based on a provided key from the query parameters.
        /// Prioritises Id first, then email address, then username.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private async Task<ApplicationUser?> GetUser(GetUserByIdQuery query)
        {
            if (!string.IsNullOrEmpty(query.Id))
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Id == query.Id);
            }

            if (!string.IsNullOrEmpty(query.EmailAddress))
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Email == query.EmailAddress);
            }

            if (!string.IsNullOrEmpty(query.Username))
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.UserName == query.Username);
            }

            return null;
        }
    }
}