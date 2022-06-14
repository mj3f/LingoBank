using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LingoBank.Core.Constants;
using LingoBank.Core.Dtos;
using LingoBank.Core.Enums;
using LingoBank.Core.Queries;
using LingoBank.Database.Contexts;
using LingoBank.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LingoBank.Core.QueryHandlers;

public class GetLanguagePhrasesQueryHandler : IRuntimeQueryHandler<GetLanguagePhrasesQuery, Paged<PhraseDto>?>
{
    private static ILogger _logger = Log.ForContext<GetLanguagePhrasesQueryHandler>();
    private readonly LingoContext _context;
    
    public GetLanguagePhrasesQueryHandler(LingoContext context) => _context = context;
    
    public async Task<Paged<PhraseDto>?> ExecuteAsync(GetLanguagePhrasesQuery query)
    {
        LanguageEntity? languageEntity =
            await _context.Languages.FirstOrDefaultAsync(x => x.Id == query.LanguageId);

        if (languageEntity is null)
        {
            _logger.Error($"[GetLanguagePhrasesQueryHandler] No language found with ID: {query.LanguageId}");
            return null;
        }

        // get 10 results per page.
        List<PhraseEntity>? phrases = languageEntity.Phrases?
            .Skip((query.Page - 1) * CoreConstants.PagedNumberOfItemsPerPage)
            .Take(CoreConstants.PagedNumberOfItemsPerPage)
            .ToList();

        if (phrases is null)
        {
            return null;
        }
        
        List<PhraseDto> phraseDtos = phrases.Select(p => new PhraseDto
        {
            Id = p.Id,
            LanguageId = p.LanguageId,
            SourceLanguage = p.SourceLanguage,
            TargetLanguage = p.TargetLanguage,
            Description = p.Description,
            Text = p.Text,
            Translation = p.Translation,
            Category = (Category) p.Category
        }).ToList();

        return new Paged<PhraseDto>(phraseDtos, languageEntity.Phrases!.Count, query.Page);
    }
}