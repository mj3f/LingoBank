using System.ComponentModel;
using System.Threading.Tasks;
using LingoBank.API.Services;
using LingoBank.Core;
using LingoBank.Core.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LingoBank.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v0/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IRuntime _runtime;
        private readonly ITokenService _tokenService;

        public AuthController(IRuntime runtime, ITokenService tokenService)
        {
            _runtime = runtime;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        [ProducesResponseType(200)]
        [Description("Returns a jwt token if the user exists.")]
        public async Task<IActionResult> Login([FromBody] UserWithPasswordDto user)
        {
            string token = await _tokenService.BuildToken(user.UserName);
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Could not generate a valid JWT Token for this user. Check that your inputs are correct.");
            }
            
            // So the use async bit in Startup.Configure can set the token in the request headers by default.
            HttpContext.Session.SetString("Token", token);

            return Ok(token);
        }
    }
}