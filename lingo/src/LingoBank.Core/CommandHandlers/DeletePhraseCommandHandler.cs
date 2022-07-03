using System;
using System.Threading.Tasks;
using LingoBank.Core.Commands;
using LingoBank.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LingoBank.Core.CommandHandlers;

public sealed class DeletePhraseCommandHandler : IRuntimeCommandHandler<DeletePhraseCommand>
{
    private readonly LingoContext _context;

    public DeletePhraseCommandHandler(LingoContext context) => _context = context;
    
    public async Task ExecuteAsync(DeletePhraseCommand command)
    {
        var phrase = await _context.Phrases.FirstOrDefaultAsync(p => p.Id == command.Id);

        if (phrase is null)
        {
            throw new RankException($"No phrase with id {command.Id} exists.");
        }

        _context.Phrases.Remove(phrase);
        await _context.SaveChangesAsync();
    }
}