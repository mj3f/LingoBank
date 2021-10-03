using LingoBank.Core.Commands;
using LingoBank.Core.Dtos;
using Xunit;

namespace LingoBank.Core.UnitTests.CommandTests
{
    public class EditUserCommandTests
    {
        private readonly EditUserCommand _sut = new();

        [Fact]
        public void Validate_ShouldReturnTrue_WhenAllInputsAreValid()
        {
            _sut.User = new UserDto
            {
                UserName = "Test",
                EmailAddress = "hello@email.com"
            };
            
            Assert.True(_sut.Validate());
        }

        [Theory]
        [InlineData("", "Hello")]
        [InlineData("2343", "")]
        [InlineData("", "")]
        public void Validate_ShouldReturnFalse_WhenOneOrMoreInputsAreInvalid(string username, string email)
        {
            _sut.User = new UserDto
            {
                UserName = username,
                EmailAddress = email
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