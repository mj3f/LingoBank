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
                LanguageDto language = await _runtime.ExecuteQueryAsync(new GetLanguageByIdQuery { Id = id });
                return Ok(language);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{languageId}/phrases")]
        [ProducesResponseType(typeof(LanguageDto), 200)]
        [ProducesResponseType(400)]
        [Description("Returns a list of phrases belonging to a language.")]
        public async Task<IActionResult> GetLanguagePhrasesAsync(string languageId)
        {
            try
            {
                List<PhraseDto> phrases = await _runtime.ExecuteQueryAsync(new GetPhrasesQuery { LanguageId = languageId });
                return Ok(phrases);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
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
        
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Description("Edits an existing language")]
        public async Task<IActionResult> EditLanguageAsync(string id, [FromBody] LanguageDto language)
        {
            try
            {
                await _runtime.ExecuteCommandAsync(new EditLanguageCommand() {Id = id, Language = language});
                return Ok("Language Modified");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}