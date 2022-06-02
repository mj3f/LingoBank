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
        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LanguageDto), 200)]
        [ProducesResponseType(400)]
        [Description("Returns a language created by the user.")]
        public async Task<IActionResult> GetLanguageByIdAsync(string id)
        {
            try
            {
                LanguageDto? language = await _runtime.ExecuteQueryAsync(new GetLanguageByIdQuery { Id = id });
                if (language is null)
                {
                    return NotFound("Language not found.");
                }

                return Ok(language);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(LanguageDto), 200)]
        [ProducesResponseType(400)]
        [Description("Creates a new language")]
        public async Task<IActionResult> CreateLanguageAsync([FromBody] LanguageDto language)
        {
            try
            {
                var (isSuccessful, message) = await _runtime.ExecuteCommandAsync(new CreateLanguageCommand { Language = language });

                if (isSuccessful)
                {
                    return Ok(language);
                }

                return BadRequest(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(LanguageDto), 200)]
        [ProducesResponseType(400)]
        [Description("Edits an existing language")]
        public async Task<IActionResult> EditLanguageAsync(string id, [FromBody] LanguageDto language)
        {
            try
            {
                var (isSuccessful, message) = await _runtime.ExecuteCommandAsync(new EditLanguageCommand { Id = id, Language = language });

                if (isSuccessful)
                {
                    return Ok(language);
                }

                return BadRequest(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}