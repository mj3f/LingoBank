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
                CreateUser = new CreateUserDto("Test user", "hello@example.com", "Hello123456!!5g"),
                Role = "User"
            };
            
            Assert.True(createUserCommand.Validate());
        }
        
        [Fact]
        public void Validate_ShouldReturnFalse()
        {
            var createUserCommandA = new CreateUserCommand { CreateUser = new CreateUserDto("", "", ""), Role = ""};

            Assert.False(createUserCommandA.Validate());
        }
    }
}