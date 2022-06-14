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

namespace LingoBank.Core.QueryHandlers;

public class GetPhrasesQueryHandler : IRuntimeQueryHandler<GetPhrasesQuery, Paged<PhraseDto>>
{
    private readonly LingoContext _context;

    public GetPhrasesQueryHandler(LingoContext context) => _context = context;
    
    public async Task<Paged<PhraseDto>> ExecuteAsync(GetPhrasesQuery query)
    {
        List<PhraseEntity>? phrasesEntities = await _context.Phrases
            .Skip((query.Page - 1) * CoreConstants.PagedNumberOfItemsPerPage)
            .Take(CoreConstants.PagedNumberOfItemsPerPage)
            .ToListAsync();

        var phrases = phrasesEntities?.Select(p => new PhraseDto
        {
            Category = (Category) p.Category,
            Description = p.Description,
            Id = p.Id,
            LanguageId = p.LanguageId,
            SourceLanguage = p.SourceLanguage,
            TargetLanguage = p.TargetLanguage,
            Text = p.Text,
            Translation = p.Translation
        }).ToList();
            
        return new Paged<PhraseDto>(phrases, _context.Phrases.Count(), query.Page);
    }
}