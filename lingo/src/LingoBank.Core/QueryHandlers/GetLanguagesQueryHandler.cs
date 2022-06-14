using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LingoBank.Core.Constants;
using LingoBank.Core.Dtos;
using LingoBank.Core.Queries;
using LingoBank.Database.Contexts;
using LingoBank.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LingoBank.Core.QueryHandlers;

public class GetLanguagesQueryHandler : IRuntimeQueryHandler<GetLanguagesQuery, Paged<LanguageDto>?>
{
    private readonly LingoContext _context;
    
    public GetLanguagesQueryHandler(LingoContext context) => _context = context;

        public async Task<Paged<LanguageDto>?> ExecuteAsync(GetLanguagesQuery query)
        {
            List<LanguageEntity>? languageEntities = await _context.Languages
                .Skip((query.Page - 1) * CoreConstants.PagedNumberOfItemsPerPage)
                .Take(CoreConstants.PagedNumberOfItemsPerPage)
                .ToListAsync();

            var languages = languageEntities?.Select(l => new LanguageDto
            {
                Code = l.Code,
                Description = l.Description,
                Id = l.Id,
                Name = l.Name,
                Phrases = null!,
                UserId = l.UserId
            }).ToList();
            
            return new Paged<LanguageDto>(languages, _context.Languages.Count(), query.Page);
        }
}