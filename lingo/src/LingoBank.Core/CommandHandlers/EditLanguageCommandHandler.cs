using System.Threading.Tasks;
using LingoBank.Core.Commands;
using LingoBank.Core.Exceptions;
using LingoBank.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LingoBank.Core.CommandHandlers
{
    public sealed class EditLanguageCommandHandler : IRuntimeCommandHandler<EditLanguageCommand>
    {
        private readonly LingoContext _lingoContext;

        public EditLanguageCommandHandler(LingoContext context) => _lingoContext = context;

        public async Task ExecuteAsync(EditLanguageCommand command)
        {
            var language = await _lingoContext.Languages
                .FirstOrDefaultAsync(x => x.Id == command.Id);

            if (language is null)
            {
                throw new RuntimeException($"No language with id {command.Id} exists");
            }
            
            language.Name = command.Language.Name;
            await _lingoContext.SaveChangesAsync();
        }
    }
}