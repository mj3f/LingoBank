using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LingoBank.API.Controllers;
using LingoBank.Core;
using LingoBank.Core.Commands;
using LingoBank.Core.Dtos;
using LingoBank.Core.Queries;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace LingoBank.API.UnitTests.Controllers
{
    public sealed class LanguagesControllerTests
    {
        private readonly LanguagesController _sut;
        private readonly LanguageDto _language;
        
        public LanguagesControllerTests()
        {
            _language = new LanguageDto
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Deutsch",
                Phrases = new List<PhraseDto>(),
                UserId = Guid.NewGuid().ToString()
            };
            
            var runtime = new Mock<IRuntime>();
            runtime.Setup(x =>
                x.ExecuteQueryAsync(It.IsAny<GetLanguagesQuery>())).ReturnsAsync(new List<LanguageDto> { _language });
            runtime.Setup(x =>
                x.ExecuteQueryAsync(It.IsAny<GetLanguageByIdQuery>())).ReturnsAsync(_language);
            runtime.Setup(x => x.ExecuteCommandAsync(It.IsAny<CreateLanguageCommand>()));
            runtime.Setup(x => x.ExecuteCommandAsync(It.IsAny<EditLanguageCommand>()));
            runtime.Setup(x =>
                x.ExecuteQueryAsync(It.IsAny<GetPhrasesQuery>())).ReturnsAsync(new List<PhraseDto>());

            _sut = new LanguagesController(runtime.Object);
        }
        
        [Fact]
        public async Task GetById_GetLanguageById_ReturnsALanguage()
        {
            IActionResult result = await _sut.GetLanguageByIdAsync(_language.Id);
            
            Assert.IsType<OkObjectResult>(result);
        }
        
        [Fact]
        public async Task GetPhrases_GetPhrasesForALanguage_ReturnsAListOfPhrases()
        {
            IActionResult result = await _sut.GetLanguagePhrasesAsync(_language.Id);
            
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateAsync_CreateANewLanguage_Successful()
        {
            IActionResult result = await _sut.CreateLanguageAsync(_language);

            Assert.IsType<OkObjectResult>(result);
        }
        
        [Fact]
        public async Task EditAsync_UpdateAnExistingLanguage_Successful()
        {
            IActionResult result = await _sut.EditLanguageAsync(_language.Id, _language);

            Assert.IsType<OkObjectResult>(result);
        }
        
    }
}