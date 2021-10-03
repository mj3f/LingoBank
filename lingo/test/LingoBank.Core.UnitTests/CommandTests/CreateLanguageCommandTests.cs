using System;
using LingoBank.Core.Commands;
using LingoBank.Core.Dtos;
using Xunit;

namespace LingoBank.Core.UnitTests.CommandTests
{
    public sealed class CreateLanguageCommandTests
    {
        private readonly CreateLanguageCommand _sut = new();

        [Fact]
        public void Validate_ShouldReturnTrue_WhenANonNullLanguageDtoWithNameProvided()
        {
            _sut.Language  = new LanguageDto
            {
                Id = Guid.NewGuid().ToString(),
                Name = "English",
                Phrases = null,
                UserId = Guid.NewGuid().ToString()
            };

            Assert.True(_sut.Validate());
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenANullLanguageDtoProvided()
        {
            Assert.False(_sut.Validate());
        }
        
        [Fact]
        public void Validate_ShouldReturnFalse_WhenANonNullLanguageDtoWithNoNameProvided()
        {
            _sut.Language  = new LanguageDto
            {
                Id = Guid.NewGuid().ToString(),
                Name = string.Empty,
                Phrases = null,
                UserId = Guid.NewGuid().ToString()
            };
            
            Assert.False(_sut.Validate());
        }
        
    }
}