using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using LingoBank.API.Controllers;
using LingoBank.Core;
using LingoBank.Core.Commands;
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
        
        [Fact]
        public async Task CreateLanguageAsync_WithLanguageObject_ShouldCreateLanguage()
        {
            _runtime.ExecuteCommandAsync(Arg.Is<CreateLanguageCommand>(command => command.Language == _language))
                .Returns(new RuntimeCommandResult(true));
            var result = (OkObjectResult) await _sut.CreateLanguageAsync(_language);

            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(_language);
        }
        
        [Fact]
        public async Task CreateLanguageAsync_WithNullObject_ShouldReturnError()
        {
            _runtime.ExecuteCommandAsync(Arg.Any<CreateLanguageCommand>())
                .Returns(new RuntimeCommandResult(false));
            var result = (BadRequestObjectResult) await _sut.CreateLanguageAsync(null);

            result.StatusCode.Should().Be(400);
        }
        
        [Fact]
        public async Task EditLanguageAsync_WithExistingLanguageObject_ShouldUpdateTheLanguage()
        {
            _runtime.ExecuteCommandAsync(Arg.Is<EditLanguageCommand>(command => command.Language == _language))
                .Returns(new RuntimeCommandResult(true));
            var result = (OkObjectResult) await _sut.EditLanguageAsync(_language.Id, _language);

            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(_language);
        }
        
        [Fact]
        public async Task EditLanguageAsync_WithLanguageObjectAndInvalidId_ShouldReturnError() // id does not match language in db.
        {
            string id = Guid.NewGuid().ToString();
            var language = new LanguageDto 
            {
                Name = "Test"
            };
            _runtime.ExecuteCommandAsync(Arg.Is<EditLanguageCommand>(command => command.Id == id && command.Language == language))
                .Returns(new RuntimeCommandResult(false));
            var result = (BadRequestObjectResult) await _sut.EditLanguageAsync(id, language);

            result.StatusCode.Should().Be(400);
        }
        
    }
}