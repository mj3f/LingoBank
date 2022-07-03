using System;
using System.ComponentModel;
using System.Threading.Tasks;
using LingoBank.API.Authorization;
using LingoBank.Core;
using LingoBank.Core.Commands;
using LingoBank.Core.Dtos;
using LingoBank.Core.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LingoBank.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{Roles.Administrator}, {Roles.User}")]
    [Route("api/v0/languages")]
    public sealed class LanguagesController : ControllerBase
    {
        private readonly IRuntime _runtime;

        public LanguagesController(IRuntime runtime) => _runtime = runtime;

        [HttpGet]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(typeof(Paged<LanguageDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Description("Returns a paginated list of all languages. Accessible by Administrators only.")]
        public async Task<IActionResult> GetLanguagesAsync([FromQuery] int page)
        {
            try
            {
                Paged<LanguageDto> languages = await _runtime.ExecuteQueryAsync(new GetLanguagesQuery {Page = page});
                
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
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
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

        [HttpGet("{id}/phrases")]
        [ProducesResponseType(typeof(Paged<PhraseDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Description("Returns a paginated list of phrases for a language.")]
        public async Task<IActionResult> GetLanguagePhrasesAsync(string id, [FromQuery] int page)
        {
            try
            {
                Paged<PhraseDto>? phrases = await _runtime.ExecuteQueryAsync(new GetLanguagePhrasesQuery
                {
                    LanguageId = id,
                    Page = page
                });

                if (phrases is null)
                {
                    return BadRequest($"No phrases found for language with id: {id}");
                }

                return Ok(phrases);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Description("Creates a new language")]
        public async Task<IActionResult> CreateLanguageAsync([FromBody] LanguageDto language)
        {
            try
            {
                await _runtime.ExecuteCommandAsync(new CreateLanguageCommand { Language = language });
                
                return Ok("Language created.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Description("Edits an existing language")]
        public async Task<IActionResult> EditLanguageAsync(string id, [FromBody] LanguageDto language)
        {
            try
            {
               await _runtime.ExecuteCommandAsync(new EditLanguageCommand { Id = id, Language = language });

               return Ok("Language updated.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType( typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Description("Deletes a language by the id provided")]
        public async Task<IActionResult> DeleteLanguageByIdAsync(string id)
        {
            try
            {
                await _runtime.ExecuteCommandAsync(new DeleteLanguageCommand { Id = id });

                return Ok("Language deleted.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}