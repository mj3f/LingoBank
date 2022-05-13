using System.Collections.Generic;
using System.Threading.Tasks;
using LingoBank.Core.Dtos;
using LingoBank.Core.Queries;
using LingoBank.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LingoBank.Core.QueryHandlers
{
    public sealed class GetLanguageByIdQueryHandler : IRuntimeQueryHandler<GetLanguageByIdQuery, LanguageDto?>
    {
        private readonly LingoContext _lingoContext;

        public GetLanguageByIdQueryHandler(LingoContext context) => _lingoContext = context;

        public async Task<LanguageDto> ExecuteAsync(GetLanguageByIdQuery query)
        {
            var languageEntity = await _lingoContext.Languages.FirstOrDefaultAsync(x => x.Id == query.Id);
            if (languageEntity != null)
            {
                return new LanguageDto
                {
                    Id = languageEntity.Id,
                    Name = languageEntity.Name
                };
            }

            return null;
        }
    }
}