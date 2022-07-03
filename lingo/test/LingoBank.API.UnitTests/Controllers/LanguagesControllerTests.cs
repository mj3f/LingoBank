using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using LingoBank.API.Controllers;
using LingoBank.Core;
using LingoBank.Core.Commands;
using LingoBank.Core.Dtos;
using LingoBank.Core.Exceptions;
using LingoBank.Core.Queries;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
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
            _runtime.When(x => x.ExecuteCommandAsync(Arg.Is<CreateLanguageCommand>(command => command.Language == _language)))
                .Do(_ => {});
            
            var result = (OkObjectResult) await _sut.CreateLanguageAsync(_language);

            result.StatusCode.Should().Be(200);
            result.Value.Should().Be("Language created.");
        }
        
        [Fact]
        public async Task CreateLanguageAsync_WithNullObject_ShouldReturnError()
        {
            _runtime.ExecuteCommandAsync(Arg.Any<CreateLanguageCommand>())
                .Throws(new RuntimeException("Error with creating language"));
            var result = (BadRequestObjectResult) await _sut.CreateLanguageAsync(null);

            result.StatusCode.Should().Be(400);
        }
        
        [Fact]
        public async Task EditLanguageAsync_WithExistingLanguageObject_ShouldUpdateTheLanguage()
        {
            _runtime.When(x => x.ExecuteCommandAsync(Arg.Is<EditLanguageCommand>(command => command.Language == _language)))
                .Do(_ => { });

            var result = (OkObjectResult) await _sut.EditLanguageAsync(_language.Id, _language);

            result.StatusCode.Should().Be(200);
            result.Value.Should().Be("Language updated.");
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
                .Throws(new RuntimeException("Error with editing language"));
            var result = (BadRequestObjectResult) await _sut.EditLanguageAsync(id, language);

            result.StatusCode.Should().Be(400);
        }
        
        [Fact]
        public async Task DeleteLanguageAsync_WithExistingLanguage_ShouldDeleteTheLanguage()
        {
            string id = Guid.NewGuid().ToString();
            _runtime.When(x => x.ExecuteCommandAsync(Arg.Is<DeleteLanguageCommand>(command => command.Id == id)))
                .Do(_ => { });

            var result = (OkObjectResult) await _sut.DeleteLanguageByIdAsync(id);

            result.StatusCode.Should().Be(200);
            result.Value.Should().Be("Language deleted.");
        }


        [Fact]
        public async Task DeleteLanguageAsync_WithExistingLanguage_ShouldThrowExceptionWhenTheLanguageDoesNotExist()
        {
            string id = Guid.NewGuid().ToString();
            _runtime.ExecuteCommandAsync(Arg.Is<DeleteLanguageCommand>(command => command.Id == id))
                .Throws(new RuntimeException("Language with that id does not exist."));

            var result = (BadRequestObjectResult) await _sut.DeleteLanguageByIdAsync(id);

            result.StatusCode.Should().Be(400);
        }

    }
}