using System;
using LingoBank.Core.Commands;
using LingoBank.Core.Dtos;
using Xunit;

namespace LingoBank.Core.UnitTests.CommandTests
{
    public class CreateUserCommandTests
    {

        [Fact]
        public void Validate_ShouldReturnTrue()
        {
            var createUserCommand = new CreateUserCommand
            {
                UserWithPassword = new UserWithPasswordDto
                {
                    EmailAddress = "hello@example.com",
                    UserName = "Test user",
                    Password = "Hello123456!!5g"
                }
            };
            
            Assert.True(createUserCommand.Validate());
        }
        
        [Theory]
        [MemberData(nameof(UserWithPasswordTestDataGenerator.GetTestData), MemberType = typeof(UserWithPasswordTestDataGenerator))]
        public void Validate_ShouldReturnFalse(UserWithPasswordDto a, UserWithPasswordDto b, UserWithPasswordDto c)
        {
            var createUserCommandA = new CreateUserCommand { UserWithPassword = a };
            var createUserCommandB = new CreateUserCommand { UserWithPassword = b };
            var createUserCommandC = new CreateUserCommand { UserWithPassword = c };

            Assert.False(createUserCommandA.Validate());
            Assert.False(createUserCommandB.Validate());
            Assert.False(createUserCommandC.Validate());
        }
    }
}