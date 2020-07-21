using System.Threading.Tasks;
using LingoBank.Core.Commands;
using LingoBank.Database.Contexts;
using LingoBank.Database.Enums;
using Microsoft.EntityFrameworkCore;

namespace LingoBank.Core.CommandHandlers
{
    public sealed class EditPhraseCommandHandler : IRuntimeCommandHandler<EditPhraseCommand>
    {
        private readonly LingoContext _lingoContext;

        public EditPhraseCommandHandler(LingoContext lingoContext) => _lingoContext = lingoContext;

        public async Task<RuntimeCommandResult> ExecuteAsync(EditPhraseCommand command)
        {
            var phrase = await _lingoContext.Phrases
                .FirstOrDefaultAsync(x => x.Id == command.Phrase.Id);

            if (phrase == null)
            {
                return new RuntimeCommandResult {Message = $"Could not find a phrase with id {command.Phrase.Id}"};
            }

            phrase.LanguageId = command.Phrase.LanguageId;
            phrase.SourceLanguage = command.Phrase.SourceLanguage;
            phrase.TargetLanguage = command.Phrase.TargetLanguage;
            phrase.Text = command.Phrase.Text;
            phrase.Translation = command.Phrase.Translation;
            phrase.Description = command.Phrase.Description;
            phrase.Category = (Category) command.Phrase.Category;
            await _lingoContext.SaveChangesAsync();

            return new RuntimeCommandResult {Message = "Phrase Modified."};
        }
    }
}