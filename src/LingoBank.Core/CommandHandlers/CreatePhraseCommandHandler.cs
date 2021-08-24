using System;
using System.Threading.Tasks;
using LingoBank.Core.Commands;
using LingoBank.Database.Contexts;
using LingoBank.Database.Entities;
using LingoBank.Database.Enums;

namespace LingoBank.Core.CommandHandlers
{
    public sealed class CreatePhraseCommandHandler : IRuntimeCommandHandler<CreatePhraseCommand>
    {
        private readonly LingoContext _lingoContext;

        public CreatePhraseCommandHandler(LingoContext context) => _lingoContext = context;

        public async Task ExecuteAsync(CreatePhraseCommand command)
        {
            await _lingoContext.Phrases.AddAsync(new PhraseEntity
            {
                Id = Guid.NewGuid().ToString(),
                LanguageId = command.Phrase.LanguageId,
                SourceLanguage = command.Phrase.SourceLanguage,
                TargetLanguage = command.Phrase.TargetLanguage,
                Text = command.Phrase.Text,
                Translation = command.Phrase.Translation,
                Description = command.Phrase.Description,
                Category = (Category)command.Phrase.Category
            });
            await _lingoContext.SaveChangesAsync();
        }
    }
}