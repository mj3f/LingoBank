using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LingoBank.Core.Dtos;
using LingoBank.Core.Enums;
using LingoBank.Core.Queries;
using LingoBank.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LingoBank.Core.QueryHandlers
{
    public sealed class GetPhrasesQueryHandler : IRuntimeQueryHandler<GetPhrasesQuery, List<PhraseDto>>
    {
        private readonly LingoContext _lingoContext;

        public GetPhrasesQueryHandler(LingoContext context) => _lingoContext = context;

        public async Task<List<PhraseDto>> ExecuteAsync(GetPhrasesQuery query)
        {
            List<PhraseDto> phrases = await _lingoContext.Phrases
                .Where(x => x.LanguageId == query.LanguageId)
                .Select(
                    x => new PhraseDto
                    {
                        Id = x.Id,
                        LanguageId = x.LanguageId,
                        SourceLanguage = x.SourceLanguage,
                        TargetLanguage = x.TargetLanguage,
                        Text = x.Text,
                        Translation = x.Translation,
                        Description = x.Description,
                        Category = (Category) x.Category
                    })
                .ToListAsync();
            
            return phrases;
        }
    }
}