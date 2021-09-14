using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LingoBank.API.Authentication;
using LingoBank.Core;
using LingoBank.Core.Dtos;
using LingoBank.Core.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace LingoBank.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
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
        

        [HttpGet("current-user")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(401)] 
        [ProducesResponseType(400)]
        [Description("Returns the current logged in users details.")]
        public async Task<IActionResult> GetCurrentUserAsync()
        {
            string? email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (email is null)
            {
                return BadRequest("No email in the jwt token found.");
            }

            var user = await _runtime.ExecuteQueryAsync(new GetUserByIdQuery { EmailAddress = email });

            // Don't assume the user exists despite a jwt token being provided -
            // what if the user is deleted and the token is still somehow valid at this point?
            if (user is null)
            {
                return BadRequest("No user found in the database with the email address provided.");
            }

            return Ok(user);
        }
        
        
        [HttpGet("refresh")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Description("Returns a refreshed jwt token (without the user needing to login again) if the user exists.")]
        public async Task<IActionResult> GetRefreshTokenAsync()
        {
            string? email = User.FindFirst(ClaimTypes.Email)?.Value;
            
            if (email is not null)
            {
                string token = await _jwtTokenGenerator.BuildToken(email);
                return Ok(token);
            }

            return BadRequest("Could not generate a refreshed JWT token.");
        }
    }
}