using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LingoBank.Core.Dtos;
using LingoBank.Core.Enums;
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
            
            if (appUser == null)
            {
                return null;
            }

            var languages = new List<LanguageDto>();
            if (query.IncludeLanguages)
            {
                var languageEntities = _context.Languages.Where(l => l.UserId == query.Id).ToList();
                
                foreach (var language in languageEntities) // Checks if at least one language is in list.
                {
                    // language phrases should be fetched separately using the GetLanguageByIdQuery.
                    languages.Add(new LanguageDto
                    {
                        Id = language.Id,
                        Name = language.Name,
                        UserId = language.UserId
                    });
                }
            }

            return new UserDto
            {
                Id = appUser.Id,
                EmailAddress = appUser.Email,
                Role = appUser.Role,
                UserName = appUser.UserName,
                Languages = languages
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