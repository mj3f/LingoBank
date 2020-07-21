using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LingoBank.Core.Dtos;
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
            var phrasesEntities = await _lingoContext.Phrases.Where(x => x.LanguageId == query.LanguageId).ToListAsync();
            var phrases = new List<PhraseDto>();
            foreach (var phrasesEntity in phrasesEntities)
            {
                phrases.Add(new PhraseDto
                {
                    Id = phrasesEntity.Id,
                    LanguageId = phrasesEntity.LanguageId,
                    SourceLanguage = phrasesEntity.SourceLanguage,
                    TargetLanguage = phrasesEntity.TargetLanguage,
                    Text = phrasesEntity.Text,
                    Translation = phrasesEntity.Translation,
                    Description = phrasesEntity.Description,
                    Category = (int) phrasesEntity.Category
                });
            }
            return phrases;
        }
    }
}