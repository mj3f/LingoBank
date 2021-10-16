using System;
using LingoBank.Core.Commands;
using LingoBank.Core.Dtos;
using Xunit;

namespace LingoBank.Core.UnitTests.CommandTests
{
    public class EditPhraseCommandTests
    {
        private readonly EditPhraseCommand _sut = new();

        [Fact]
        public void Validate_ShouldReturnTrue_WhenAllInputsAreValid()
        {
            _sut.Id = "Hi";
            _sut.Phrase = new PhraseDto
            {
                Text = "hello",
                Translation = "Ciao",
                SourceLanguage = "English",
                TargetLanguage = "Italian",
                LanguageId = Guid.NewGuid().ToString(),
            };
            
            Assert.True(_sut.Validate());
        }

        [Theory]
        [InlineData("", "English", "Italian", "Hello", "Ciao", "dtgftrtrtrt")]
        [InlineData("grhfghfh", "", "Italian", "Hello", "Ciao", "dtgftrtrtrt")]
        [InlineData("grhfghfh", "English", "", "Hello", "Ciao", "dtgftrtrtrt")]
        [InlineData("grhfghfh", "English", "Italian", "", "Ciao", "dtgftrtrtrt")]
        [InlineData("grhfghfh", "English", "Italian", "Hello", "", "dtgftrtrtrt")]
        [InlineData("grhfghfh", "English", "Italian", "Hello", "Ciao", "")]
        [InlineData("", "", "", "", "", "")]
        public void Validate_ShouldReturnFalse_WhenOneOrMoreInputsAreInvalid(
            string id,
            string text,
            string translation,
            string source,
            string target,
            string languageId)
        {
            _sut.Id = id;
            _sut.Phrase = new PhraseDto
            {
                Text = text,
                Translation = translation,
                SourceLanguage = source,
                TargetLanguage = target,
                LanguageId = languageId,
            };
            
            Assert.False(_sut.Validate());
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenLanguageDtoIsNull()
        {
            Assert.False(_sut.Validate());
        }
    }
}