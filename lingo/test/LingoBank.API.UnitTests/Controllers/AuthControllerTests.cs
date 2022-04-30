using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using LingoBank.API.Authentication;
using LingoBank.API.Controllers;
using LingoBank.Core;
using LingoBank.Core.Dtos;
using LingoBank.Core.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace LingoBank.API.UnitTests.Controllers
{
    public sealed class AuthControllerTests
    {
        private readonly AuthController _sut;
        private readonly IRuntime _runtime = Substitute.For<IRuntime>();
        private readonly IJwtTokenGenerator _jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();

        public AuthControllerTests()
        {
            _sut = new AuthController(_runtime, _jwtTokenGenerator);
        }

        [Fact]
        public async Task Login_LoginWithValidUser_ShouldReturnAJwtToken()
        {
            var validEmail = "test@example.com";
            var validPassword = "TestPassword12345";
            var user = new UserDto
            {
                EmailAddress = validEmail
            };
            var signInResult = SignInResult.Success;
            var mockJwtToken = Guid.NewGuid().ToString();

            _jwtTokenGenerator.BuildToken(validEmail).Returns(mockJwtToken);
            _runtime.ExecuteQueryAsync(Arg.Any<GetUserByIdQuery>()).Returns(user);
            _runtime.ExecuteQueryAsync(Arg.Any<SignInUserQuery>()).Returns(signInResult);

            var result = (OkObjectResult) await _sut.Login(new LoginDto(validEmail, validPassword));

            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(mockJwtToken);
        }

        [Fact]
        public async Task Login_LoginWithInvalidUser_ShouldReturnError()
        {
            _jwtTokenGenerator.BuildToken(Arg.Any<string>()).Returns(string.Empty);
           
            var result = (BadRequestObjectResult) await _sut.Login(new LoginDto(string.Empty, string.Empty));

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task GetCurrentUserAsync_WithExistingUser_ShouldReturnCurrentUser()
        {
            var email = "test@example.com";
            var user = new UserDto
            {
                EmailAddress = email
            };
            
            _sut.ControllerContext = SetupSystemUnderTestClaimsPrinciple(email);
            
            _runtime.ExecuteQueryAsync(Arg.Any<GetUserByIdQuery>()).Returns(user);

            var result = (OkObjectResult) await _sut.GetCurrentUserAsync();

            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(user);

        }

        [Fact]
        public async Task GetRefreshTokenAsync_WithCurrentUser_ShouldReturnJwtToken()
        {
            var email = "test@example.com";
            var mockJwtToken = Guid.NewGuid().ToString();
            
            _sut.ControllerContext = SetupSystemUnderTestClaimsPrinciple(email);
            _jwtTokenGenerator.BuildToken(email).Returns(mockJwtToken);

            var result = (OkObjectResult) await _sut.GetRefreshTokenAsync();

            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(mockJwtToken);

        }

        [Fact]
        public async Task GetRefreshTokenAsync_WithoutUser_ShouldReturnBadRequestResponse()
        {
            _sut.ControllerContext = SetupSystemUnderTestClaimsPrinciple(string.Empty);
            var result = (BadRequestObjectResult) await _sut.GetRefreshTokenAsync();

            result.StatusCode.Should().Be(400);
            result.Value.Should().Be("Could not generate a refreshed JWT token.");
        }

        private ControllerContext SetupSystemUnderTestClaimsPrinciple(string email)
        {
            var claims = new List<Claim>() 
            { 
                new Claim(ClaimTypes.Name, "username"),
                new Claim(ClaimTypes.Email, email)
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            
            return new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = claimsPrincipal
                }
            };
        }
    }
}