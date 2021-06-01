using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LingoBank.Core.Commands;
using LingoBank.Database.Contexts;
using LingoBank.Database.Entities;

namespace LingoBank.Core.CommandHandlers
{
    public sealed class CreateLanguageCommandHandler : IRuntimeCommandHandler<CreateLanguageCommand>
    {
        private readonly LingoContext _lingoContext;

        public CreateLanguageCommandHandler(LingoContext context) => _lingoContext = context;

        public async Task<RuntimeCommandResult> ExecuteAsync(CreateLanguageCommand command)
        {
            await _lingoContext.Languages.AddAsync(new LanguageEntity
            {
                Id = Guid.NewGuid().ToString(),
                Name = command.Language.Name,
                Phrases = new List<PhraseEntity>()
            });
            await _lingoContext.SaveChangesAsync();

            return new RuntimeCommandResult {Message = "Language created"};
        }
    }
}