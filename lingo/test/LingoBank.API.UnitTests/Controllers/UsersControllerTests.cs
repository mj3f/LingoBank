using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LingoBank.API.Controllers;
using LingoBank.Core;
using LingoBank.Core.Commands;
using LingoBank.Core.Dtos;
using LingoBank.Core.Queries;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace LingoBank.API.UnitTests.Controllers
{
    public class UsersControllerTests
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturnUsers()
        {
            // Arrange
            var runtime = new Mock<IRuntime>();
            runtime.Setup(x => x.ExecuteQueryAsync(It.IsAny<GetUsersQuery>())).ReturnsAsync(new List<UserDto>());

            var sut = new UsersController(runtime.Object);

            // act
            IActionResult result = await sut.GetAll();

            // assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUser()
        {
            string id = Guid.NewGuid().ToString();
            var user = new UserDto
            {
                Id = id,
                EmailAddress = "test@example.com",
                UserName = "Test",
                Role = "User"
            };
            
            var runtime = new Mock<IRuntime>();
            runtime.Setup(x => x.ExecuteQueryAsync(It.IsAny<GetUserByIdQuery>())).ReturnsAsync(user);

            var sut = new UsersController(runtime.Object);
            IActionResult result = await sut.GetById(id);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateUser()
        {
            // Arrange
            var user = new UserWithPasswordDto
            {
                Id = Guid.NewGuid().ToString(),
                EmailAddress = "test@example.com",
                UserName = "Test",
                Role = "User",
                Password = "Test12345678"
            };
           
            var runtime = new Mock<IRuntime>();
            
            // Don't need to mock this as a command does not return anything. (so we assume the input is valid for this test)
            // runtime.Setup(x => x.ExecuteCommandAsync(It.IsAny<CreateUserCommand>()));

            var sut = new UsersController(runtime.Object);
            
            // Act
            IActionResult result = await sut.Create(user);
            
            // Assert (assume the user was valid and it was created.
            string okRes = (result as OkObjectResult)?.Value as string ?? string.Empty;
            
            Assert.Equal("User created.", okRes);
        }

        [Fact]
        public async Task CreateAsync_WithNullInput_ShouldNotCreateUser()
        {
            // Arrange
            UserWithPasswordDto user = null;
           
            var runtime = new Mock<IRuntime>();
            runtime.Setup(x => x.ExecuteCommandAsync(It.IsAny<CreateUserCommand>())).ThrowsAsync(new Exception());
            
            var sut = new UsersController(runtime.Object);
            
            // Act
            IActionResult result = await sut.Create(user);
            
            // Assert (assume the user was valid and it was created.
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}