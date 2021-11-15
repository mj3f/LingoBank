using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LingoBank.Core.Dtos;
using LingoBank.Core.Queries;
using LingoBank.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LingoBank.Core.QueryHandlers
{
    public sealed class GetUsersQueryHandler : IRuntimeQueryHandler<GetUsersQuery, List<UserDto>>
    {
        private readonly LingoContext _context;

        public GetUsersQueryHandler(LingoContext context) => _context = context;


        public async Task<List<UserDto>> ExecuteAsync(GetUsersQuery query)
        {
            var appUsers = await _context.Users.ToListAsync();

            return appUsers.Select(user => new UserDto
            {
                Id = user.Id,
                EmailAddress = user.Email,
                Role = user.Role,
                UserName = user.UserName
            }).ToList();
        }
    }
}