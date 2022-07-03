using System.Threading.Tasks;
using LingoBank.Core.Commands;
using LingoBank.Core.Exceptions;
using LingoBank.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LingoBank.Core.CommandHandlers;

public sealed class DeleteLanguageCommandHandler : IRuntimeCommandHandler<DeleteLanguageCommand>
{
    private readonly LingoContext _context;

    public DeleteLanguageCommandHandler(LingoContext context) => _context = context;
    
    public async Task ExecuteAsync(DeleteLanguageCommand command)
    {
        var language = await _context.Languages.FirstOrDefaultAsync(l => l.Id == command.Id);

        if (language is null)
        {
            throw new RuntimeException($"No language with id {command.Id} exists.");
        }

        _context.Languages.Remove(language);
        await _context.SaveChangesAsync();
    }
}