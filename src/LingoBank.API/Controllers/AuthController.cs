using System.ComponentModel;
using System.Threading.Tasks;
using LingoBank.API.Authentication;
using LingoBank.Core;
using LingoBank.Core.Dtos;
using LingoBank.Core.Queries;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace LingoBank.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v0/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IRuntime _runtime;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthController(IRuntime runtime, IJwtTokenGenerator jwtTokenGenerator)
        {
            _runtime = runtime;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost("login")]
        [ProducesResponseType(200)]
        [Description("Returns a jwt token if the user exists.")]
        public async Task<IActionResult> Login([FromBody] UserWithPasswordDto userWithPassword)
        {

            UserDto user = await _runtime.ExecuteQueryAsync(new GetUserByIdQuery
                { EmailAddress = userWithPassword.EmailAddress });

            if (user is null)
            {
                return BadRequest("No user found.");
            }

            SignInResult signInResult = await _runtime.ExecuteQueryAsync(new SignInUserQuery
            {
                User = user,
                Password = userWithPassword.Password
            });
            
            if (!signInResult.Succeeded)
            {
                return BadRequest("Login credentials invalid.");
            }
            
            string token = await _jwtTokenGenerator.BuildToken(user.EmailAddress);

            return Ok(token);
        }
        
        // TODO: Endpoint for getting current logged in user.
        
        // TODO: Endpoint for getting a refresh token.
    }
}