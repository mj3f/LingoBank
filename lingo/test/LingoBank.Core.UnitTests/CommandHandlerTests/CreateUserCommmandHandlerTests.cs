using System;
using System.Threading.Tasks;
using LingoBank.Core.CommandHandlers;
using LingoBank.Core.Commands;
using LingoBank.Core.Dtos;
using LingoBank.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace LingoBank.Core.UnitTests.CommandHandlerTests
{
    public sealed class CreateUserCommmandHandlerTests
    {
    
        [Fact]
        public async Task ExecuteAsync_ShouldCreateUser()
        {
            // arrange
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var sut = new CreateUserCommandHandler(mockUserManager.Object);
            IdentityResult commandExecutionResult = null;
            
            // act
            await sut.ExecuteAsync(new CreateUserCommand
            {
                UserWithPassword = new UserWithPasswordDto
                {
                    EmailAddress = "testuser@example.com",
                    UserName = "testuser",
                    Role = "User",
                    Password = "Demo12345678"
                },
                HandleResult = (res) => commandExecutionResult = res
            });
            
            // Test will fail if result is null.
            if (commandExecutionResult is null)
            {
                Assert.NotNull(commandExecutionResult);
            }
            
            // assert
            Assert.True(commandExecutionResult.Succeeded);
        }
    }
}