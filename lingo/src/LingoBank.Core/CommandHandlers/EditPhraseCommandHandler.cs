using System.Threading.Tasks;
using LingoBank.Core.Commands;
using LingoBank.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LingoBank.Core.CommandHandlers
{
    public sealed class EditPhraseCommandHandler : IRuntimeCommandHandler<EditPhraseCommand>
    {
        private readonly LingoContext _lingoContext;

        public EditPhraseCommandHandler(LingoContext lingoContext) => _lingoContext = lingoContext;

        public async Task ExecuteAsync(EditPhraseCommand command)
        {
            var phrase = await _lingoContext.Phrases
                .FirstOrDefaultAsync(x => x.Id == command.Id);
            
            phrase.LanguageId = command.Phrase.LanguageId;
            phrase.SourceLanguage = command.Phrase.SourceLanguage;
            phrase.TargetLanguage = command.Phrase.TargetLanguage;
            phrase.Text = command.Phrase.Text;
            phrase.Translation = command.Phrase.Translation;
            phrase.Description = command.Phrase.Description;
            phrase.Category = (int) command.Phrase.Category;
            await _lingoContext.SaveChangesAsync();
        }
    }
}