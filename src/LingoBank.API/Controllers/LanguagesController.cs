using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using LingoBank.Core;
using LingoBank.Core.Commands;
using LingoBank.Core.Dtos;
using LingoBank.Core.Queries;
using Microsoft.AspNetCore.Mvc;

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
                await _runtime.ExecuteCommandAsync(new CreateLanguageCommand {Language = language});
                return Ok("Language created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<LanguageDto>), 200)]
        [ProducesResponseType(400)]
        [Description("Returns a list of all languages created by the user.")]
        public async Task<IActionResult> GetLanguagesAsync()
        {
            try
            {
                List<LanguageDto> languages = await _runtime.ExecuteQueryAsync(new GetLanguagesQuery());
                return Ok(languages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LanguageDto), 200)]
        [ProducesResponseType(400)]
        [Description("Returns a language created by the user.")]
        public async Task<IActionResult> GetLanguageByIdAsync(string id)
        {
            try
            {
                LanguageDto language = await _runtime.ExecuteQueryAsync(new GetLanguageByIdQuery { Id = id });
                return Ok(language);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}