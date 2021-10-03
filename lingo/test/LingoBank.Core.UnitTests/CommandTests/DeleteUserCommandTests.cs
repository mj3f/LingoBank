using LingoBank.Core.Commands;
using Xunit;

namespace LingoBank.Core.UnitTests.CommandTests
{
    public class DeleteUserCommandTests
    {
        [Fact]
        public void Validate_ShouldReturnTrue_WhenIdIsNotNull()
        {
            var sut = new DeleteUserCommand
            {
                Id = "Hello12"
            };
            
            Assert.True(sut.Validate());
        }
    }
}