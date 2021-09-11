using System.Threading.Tasks;
using LingoBank.API.Authentication;
using LingoBank.Core;
using LingoBank.Core.Dtos;
using LingoBank.Core.Queries;
using Moq;
using Xunit;

namespace LingoBank.API.UnitTests.Controllers
{
    public sealed class AuthControllerTests
    {
        // private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock = new();
        // private readonly Mock<IRuntime> _runtimeMock = new();

        private readonly IRuntime _runtime;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthControllerTests(IRuntime runtime, IJwtTokenGenerator jwtTokenGenerator)
        {
            _runtime = runtime;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [Fact]
        public async Task Login_LoginWithUserThatDoesNotExist_UserIsNull()
        {
            UserDto user = await _runtime.ExecuteQueryAsync(new GetUserByIdQuery
                { EmailAddress = "dummyemail@dummy.com" });

            Assert.Null(user);
        }

        [Fact]
        public async Task Login_LoginWithDefaultAdminUser_ShouldReturnAJwtToken()
        {
            UserDto user = await _runtime.ExecuteQueryAsync(new GetUserByIdQuery
                { EmailAddress = "admin@example.com" });

            string token = await _jwtTokenGenerator.BuildToken(user.EmailAddress);
            
            Assert.NotNull(token);
            
        }
    }
}