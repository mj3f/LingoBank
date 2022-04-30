// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using LingoBank.API.Controllers;
// using LingoBank.Core;
// using LingoBank.Core.Commands;
// using LingoBank.Core.Dtos;
// using LingoBank.Core.Queries;
// using Microsoft.AspNetCore.Mvc;
// using Moq;
// using Xunit;
//
// namespace LingoBank.API.UnitTests.Controllers
// {
//     public class UsersControllerTests
//     {
//         private readonly UsersController _sut;
//         private readonly UserDto _user;
//         private readonly UserWithPasswordDto _userWithPassword;
//
//         public UsersControllerTests()
//         {
//             
//             _userWithPassword = new UserWithPasswordDto
//             {
//                 Id = Guid.NewGuid().ToString(),
//                 EmailAddress = "test@example.com",
//                 UserName = "Test",
//                 Role = "User",
//                 Password = "Test12345678"
//             };
//
//             _user = _userWithPassword;
//             
//             
//             var runtime = new Mock<IRuntime>();
//             runtime.Setup(x => x.ExecuteQueryAsync(It.IsAny<GetUsersQuery>())).ReturnsAsync(new List<UserDto>());
//             runtime.Setup(x => x.ExecuteQueryAsync(It.IsAny<GetUserByIdQuery>())).ReturnsAsync(_user);
//             runtime.Setup(x => x.ExecuteQueryAsync(It.IsAny<GetLanguagesQuery>())).ReturnsAsync(new List<LanguageDto>());
//
//                 
//             _sut = new UsersController(runtime.Object);
//         }
//
//         [Fact]
//         public async Task GetAllAsync_ShouldReturnUsers()
//         {
//             // act
//             IActionResult result = await _sut.GetAll();
//
//             // assert
//             Assert.IsType<OkObjectResult>(result);
//         }
//
//         [Fact]
//         public async Task GetByIdAsync_ShouldReturnUser()
//         {
//             IActionResult result = await _sut.GetById(_user.Id);
//
//             Assert.IsType<OkObjectResult>(result);
//         }
//         
//         [Fact]
//         public async Task GetLanguagesAsync_GetListOfLanguagesForUser_ShouldReturnAListOfLanguages()
//         {
//             IActionResult result = await _sut.GetLanguagesAsync(_user.Id);
//
//             Assert.IsType<OkObjectResult>(result);
//         }
//
//         [Fact]
//         public async Task CreateAsync_ShouldCreateUser()
//         {
//             
//             IActionResult result = await _sut.Create(_userWithPassword);
//             
//             // Assert (assume the user was valid and it was created.
//             string okRes = (result as OkObjectResult)?.Value as string ?? string.Empty;
//             
//             Assert.Equal("User created.", okRes);
//         }
//     }
// }