using System.Collections.Generic;
using System.Threading.Tasks;
using LingoBank.Core.Dtos;
using LingoBank.Core.Queries;
using LingoBank.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LingoBank.Core.QueryHandlers
{
    public sealed class GetLanguagesQueryHandler : IRuntimeQueryHandler<GetLanguagesQuery, List<LanguageDto>>
    {
        private readonly LingoContext _lingoContext;

        public GetLanguagesQueryHandler(LingoContext context) => _lingoContext = context;

        public async Task<List<LanguageDto>> ExecuteAsync(GetLanguagesQuery query)
        {
            var languageEntities = await _lingoContext.Languages.ToListAsync();
            var languages = new List<LanguageDto>();
            
            foreach (var language in languageEntities)
            {
                var l = new LanguageDto
                {
                    Id = language.Id,
                    Name = language.Name,
                };
                languages.Add(l);
            }
            return languages;
        }
    }
}