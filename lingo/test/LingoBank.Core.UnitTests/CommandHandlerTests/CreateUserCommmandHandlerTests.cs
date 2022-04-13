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
                }
            });
            
            // Test will fail if result is null.
            if (commandExecutionResult is null)
            {
                Assert.NotNull(commandExecutionResult);
            }
            
            // assert
            Assert.True(commandExecutionResult.Succeeded);
        }
        
        [Theory]
        [InlineData("t")]
        [InlineData("test")]
        [InlineData("12345678")]
        [InlineData("abcdefghijklmnop")]
        [InlineData("DEMOPURPOSESBABY")]
        [InlineData("demo9876543210")]
        public async Task ExecuteAsync_ShouldFailWithInvalidPasswordInput(string input)
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed());

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
                    Password = input
                }
            });
            
            // Test will fail if result is null.
            if (commandExecutionResult is null)
            {
                Assert.NotNull(commandExecutionResult);
            }
            
            // assert
            Assert.False(commandExecutionResult.Succeeded);
        }
        
        [Theory]
        [InlineData("hello")]
        [InlineData("hello@")]
        [InlineData("hello@example")]
        [InlineData("hello@example.")]
        [InlineData("hello@example.c")]
        public async Task ExecuteAsync_ShouldFailWithInvalidEmailAddressInput(string input)
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed());

            var sut = new CreateUserCommandHandler(mockUserManager.Object);
            IdentityResult commandExecutionResult = null;
            
            // act
            await sut.ExecuteAsync(new CreateUserCommand
            {
                UserWithPassword = new UserWithPasswordDto
                {
                    EmailAddress = input,
                    UserName = "testuser",
                    Role = "User",
                    Password = "Demo1344566756565"
                }
            });
            
            // Test will fail if result is null.
            if (commandExecutionResult is null)
            {
                Assert.NotNull(commandExecutionResult);
            }
            
            // assert
            Assert.False(commandExecutionResult.Succeeded);
        }
    }
}