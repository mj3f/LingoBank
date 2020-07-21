using System;
using System.ComponentModel;
using System.Threading.Tasks;
using LingoBank.Core;
using LingoBank.Core.Commands;
using LingoBank.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace LingoBank.API.Controllers
{
    [Route("api/v0/languages")]
    [Produces("application/json")]
    [ApiController]
    public sealed class LanguagesController : ControllerBase
    {
        private readonly IRuntime _runtime;

        public LanguagesController(IRuntime runtime) => _runtime = runtime;

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Description("Creates a new language")]
        public async Task<IActionResult> CreateLanguageAsync([FromBody] LanguageDto language)
        {
            try
            {
                await _runtime.ExecuteCommandAsync(new CreateLanguageCommand {LanguageDto = language});
                return Ok("Language created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}