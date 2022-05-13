using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using LingoBank.API.Controllers;
using LingoBank.Core;
using LingoBank.Core.Dtos;
using LingoBank.Core.Queries;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace LingoBank.API.UnitTests.Controllers
{
    public sealed class LanguagesControllerTests
    {
        private readonly LanguagesController _sut;
        private readonly IRuntime _runtime = Substitute.For<IRuntime>();
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

            _sut = new LanguagesController(_runtime);
        }
        
        [Fact]
        public async Task GetLanguageByIdAsync_WithId_ShouldReturnLanguage()
        {
            _runtime.ExecuteQueryAsync(Arg.Is<GetLanguageByIdQuery>(query => query.Id == _language.Id)).Returns(_language);
            _runtime.ExecuteQueryAsync(Arg.Is<GetPhrasesQuery>(query => query.LanguageId == _language.Id)).Returns(_language.Phrases as List<PhraseDto>);
            var result = (OkObjectResult) await _sut.GetLanguageByIdAsync(_language.Id);
            
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(_language);
        }
        
        [Fact]
        public async Task GetLanguageByIdAsync_WithUnknownId_ShouldReturnError()
        {
            var id = Guid.NewGuid().ToString();
            _runtime.ExecuteQueryAsync(Arg.Is<GetLanguageByIdQuery>(query => query.Id == id)).ReturnsNull();
            var result = (NotFoundObjectResult) await _sut.GetLanguageByIdAsync(id);
            
            result.StatusCode.Should().Be(404);
        }
        
        [Fact(Skip = "Remove Moq")]
        public async Task CreateAsync_CreateANewLanguage_Successful()
        {
            IActionResult result = await _sut.CreateLanguageAsync(_language);

            Assert.IsType<OkObjectResult>(result);
        }
        
        [Fact(Skip = "Remove Moq")]
        public async Task EditAsync_UpdateAnExistingLanguage_Successful()
        {
            IActionResult result = await _sut.EditLanguageAsync(_language.Id, _language);

            Assert.IsType<OkObjectResult>(result);
        }
        
    }
}