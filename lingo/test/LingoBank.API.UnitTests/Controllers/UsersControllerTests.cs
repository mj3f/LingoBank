using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LingoBank.API.Controllers;
using LingoBank.Core;
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
    }
}