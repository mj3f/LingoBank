using System;
using LingoBank.Core.Commands;
using LingoBank.Core.Dtos;
using Xunit;

namespace LingoBank.Core.UnitTests.CommandTests
{
    public class CreatePhraseCommandTests
    {
        private readonly CreatePhraseCommand _sut = new();

        [Fact]
        public void Validate_ShouldReturnTrue_WhenAllInputsAreValid()
        {
            _sut.Phrase = new PhraseDto
            {
                Category = 0,
                Description = "Test desc",
                Text = "hello",
                Translation = "Ciao",
                SourceLanguage = "English",
                TargetLanguage = "Italian",
                LanguageId = Guid.NewGuid().ToString(),
                Id = Guid.NewGuid().ToString()
            };

            Assert.True(_sut.Validate());
        }
        
        [Fact]
        public void Validate_ShouldReturnFalse_WhenPhraseIsNull()
        {
            Assert.False(_sut.Validate());
        }
        
        [Theory]
        [InlineData("", "English", "Italian", "Hello", "Ciao")]
        [InlineData("grhfghfh", "", "Italian", "Hello", "Ciao")]
        [InlineData("grhfghfh", "English", "", "Hello", "Ciao")]
        [InlineData("grhfghfh", "English", "Italian", "", "Ciao")]
        [InlineData("grhfghfh", "English", "Italian", "Hello", "")]
        [InlineData("", "", "", "", "")]
        public void Validate_ShouldReturnFalse_WhenInputsAreInvalid(
            string languageId,
            string sourceLanguage,
            string targetLanguage,
            string text,
            string translation)
        {
            _sut.Phrase = new PhraseDto
            {
                Category = 0,
                Text = text,
                Translation = translation,
                SourceLanguage = sourceLanguage,
                TargetLanguage = targetLanguage,
                LanguageId = languageId,
                Id = Guid.NewGuid().ToString()
            };

            Assert.False(_sut.Validate());
        }
    }
}