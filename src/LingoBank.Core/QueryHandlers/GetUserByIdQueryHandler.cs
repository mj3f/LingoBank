using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LingoBank.Core.Dtos;
using LingoBank.Core.Queries;
using LingoBank.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LingoBank.Core.QueryHandlers
{
    public sealed class GetUserByIdQueryHandler : IRuntimeQueryHandler<GetUserByIdQuery, UserDto>
    {
        private readonly LingoContext _context;

        public GetUserByIdQueryHandler(LingoContext context) => _context = context;


        public async Task<UserDto> ExecuteAsync(GetUserByIdQuery query)
        {
            var appUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == query.Id);

            if (appUser == null)
            {
                return null;
            }

            var languageEntities = _context.Languages.Where(l => l.UserId == query.Id).ToList();

            var languages = new List<LanguageDto>();
            foreach (var language in languageEntities) // Checks if at least one language is in list.
            {
                languages.Add(new LanguageDto
                {
                    Id = language.Id,
                    Name = language.Name,
                    UserId = language.UserId,
                    Phrases = language.Phrases?.Select(p => new PhraseDto
                    {
                        Id = p.Id,
                        LanguageId = p.LanguageId,
                        SourceLanguage = p.SourceLanguage,
                        TargetLanguage = p.TargetLanguage,
                        Description = p.Description,
                        Text = p.Text,
                        Translation = p.Translation,
                        Category = (int) p.Category
                    }).ToList()
                });
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
    }
}