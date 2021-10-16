using LingoBank.Core.Commands;
using LingoBank.Core.Dtos;
using Xunit;

namespace LingoBank.Core.UnitTests.CommandTests
{
    public class EditLanguageCommandTests
    {
        private readonly EditLanguageCommand _sut = new();

        [Fact]
        public void Validate_ShouldReturnTrue_WhenAllInputsAreValid()
        {
            _sut.Id = "Hi";
            _sut.Language = new LanguageDto
            {
                Name = "Test"
            };
            
            Assert.True(_sut.Validate());
        }

        [Theory]
        [InlineData("", "Hello")]
        [InlineData("2343", "")]
        [InlineData("", "")]
        public void Validate_ShouldReturnFalse_WhenOneOrMoreInputsAreInvalid(string id, string name)
        {
            _sut.Id = id;
            _sut.Language = new LanguageDto { Name = name };
            
            Assert.False(_sut.Validate());
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenLanguageDtoIsNull()
        {
            Assert.False(_sut.Validate());
        }
    }
}