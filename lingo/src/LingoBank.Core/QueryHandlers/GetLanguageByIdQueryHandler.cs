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
    public sealed class GetLanguageByIdQueryHandler : IRuntimeQueryHandler<GetLanguageByIdQuery, LanguageDto?>
    {
        private readonly LingoContext _lingoContext;

        public GetLanguageByIdQueryHandler(LingoContext context) => _lingoContext = context;

        public async Task<LanguageDto?> ExecuteAsync(GetLanguageByIdQuery query)
        {
            var languageEntity = await _lingoContext.Languages.FirstOrDefaultAsync(x => x.Id == query.Id);

            if (languageEntity is null)
            {
                return null;
            }
            
            return new LanguageDto
            {
                Id = languageEntity.Id,
                Name = languageEntity.Name,
                UserId = languageEntity.UserId,
                Phrases = languageEntity.Phrases?.Select(p => new PhraseDto
                {
                    Id = p.Id,
                    LanguageId = p.LanguageId,
                    SourceLanguage = p.SourceLanguage,
                    TargetLanguage = p.TargetLanguage,
                    Description = p.Description,
                    Text = p.Text,
                    Translation = p.Translation,
                    Category = (Category) p.Category
                }).ToList() ?? new List<PhraseDto>()
            };
        }
    }
}