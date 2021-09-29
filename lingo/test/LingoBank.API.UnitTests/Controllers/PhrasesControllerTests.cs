using System;
using System.Threading.Tasks;
using LingoBank.API.Controllers;
using LingoBank.Core;
using LingoBank.Core.Commands;
using LingoBank.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace LingoBank.API.UnitTests.Controllers
{
    public sealed class PhrasesControllerTests
    {
        private readonly PhrasesController _sut;
        private readonly PhraseDto _phrase;
        
        public PhrasesControllerTests()
        {
            var runtime = new Mock<IRuntime>();
            runtime.Setup(x => x.ExecuteCommandAsync(It.IsAny<CreatePhraseCommand>()));
            runtime.Setup(x => x.ExecuteCommandAsync(It.IsAny<EditPhraseCommand>()));

            _sut = new PhrasesController(runtime.Object);

            _phrase = new PhraseDto
            {
                Category = 0,
                Description = "Test desc",
                Id = Guid.NewGuid().ToString(),
                LanguageId = Guid.NewGuid().ToString(),
                SourceLanguage = "English",
                TargetLanguage = "German",
                Text = "This is a test, translate that!",
                Translation = "Dies ist ein Test, übersetzen Sie das!",
            };
        }

        [Fact]
        public async Task CreateAsync_CreateANewPhrase_Successful()
        {
            
            IActionResult result = await _sut.CreatePhraseAsync(_phrase);
            
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task EditAsync_EditAnExistingPhrase_Successful()
        {
            IActionResult result = await _sut.EditPhraseAsync(_phrase.Id, _phrase);
            
            Assert.IsType<OkObjectResult>(result);
        }
    }
}