using LingoBank.Core;
using Microsoft.AspNetCore.Mvc;

namespace LingoBank.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v0/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IRuntime _runtime;

        public AuthController(IRuntime runtime) => _runtime = runtime;
        
        // TODO: Endpoint to take user email/username and password and return a jwt token.
    }
}