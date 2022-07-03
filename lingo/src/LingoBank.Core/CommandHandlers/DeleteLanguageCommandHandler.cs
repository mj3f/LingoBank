using System.Threading.Tasks;
using LingoBank.Core.Commands;
using LingoBank.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LingoBank.Core.CommandHandlers;

public sealed class DeleteLanguageCommandHandler : IRuntimeCommandHandler<DeleteLanguageCommand>
{
    private readonly LingoContext _context;

    public DeleteLanguageCommandHandler(LingoContext context) => _context = context;
    
    public async Task<RuntimeCommandResult> ExecuteAsync(DeleteLanguageCommand command)
    {
        var language = await _context.Languages.FirstOrDefaultAsync(l => l.Id == command.Id);

        if (language is null)
        {
            return new RuntimeCommandResult(false, "Language does not exist");
        }

        _context.Languages.Remove(language);
        await _context.SaveChangesAsync();

        return new RuntimeCommandResult(true, string.Empty);
    }
}