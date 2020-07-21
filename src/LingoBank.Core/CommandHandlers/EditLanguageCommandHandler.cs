using System.Threading.Tasks;
using LingoBank.Core.Commands;
using LingoBank.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LingoBank.Core.CommandHandlers
{
    public sealed class EditLanguageCommandHandler : IRuntimeCommandHandler<EditLanguageCommand>
    {
        private readonly LingoContext _lingoContext;

        public EditLanguageCommandHandler(LingoContext context) => _lingoContext = context;

        public async Task<RuntimeCommandResult> ExecuteAsync(EditLanguageCommand command)
        {
            var language = await _lingoContext.Languages
                .FirstOrDefaultAsync(x => x.Id == command.Id);

            if (language == null)
            {
                return new RuntimeCommandResult {Message = $"Could not find a language with id {command.Language.Id}"};
            }

            language.Name = command.Language.Name;
            await _lingoContext.SaveChangesAsync();

            return new RuntimeCommandResult {Message = "Language Modified."};
        }
    }
}