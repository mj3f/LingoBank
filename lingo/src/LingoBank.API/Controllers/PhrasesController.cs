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
    [Route("api/v0/phrases")]
    public sealed class PhrasesController : ControllerBase
    {
        private readonly IRuntime _runtime;

        public PhrasesController(IRuntime runtime) => _runtime = runtime;
        
        [HttpGet]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(typeof(Paged<PhraseDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Description("Returns a paginated list of all phrases. Accessible by Administrators only.")]
        public async Task<IActionResult> GetPhrasesAsync([FromQuery] int page)
        {
            try
            {
                Paged<PhraseDto> phrases = await _runtime.ExecuteQueryAsync(new GetPhrasesQuery { Page = page });

                return Ok(phrases);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(PhraseDto), 200)]
        [ProducesResponseType(400)]
        [Description("Creates a new phrase for a language")]
        public async Task<IActionResult> CreatePhraseAsync([FromBody] PhraseDto phrase)
        {
            try
            {
                var (isSuccessful, message) = await _runtime.ExecuteCommandAsync(new CreatePhraseCommand { Phrase = phrase });

                if (isSuccessful)
                {
                    return Ok(phrase); // How to return the phrase created, or at least the ID from the command, at this point the id property is still null when returning this.
                }

                return BadRequest(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PhraseDto), 200)]
        [ProducesResponseType(400)]
        [Description("Edits an existing phrase")]
        public async Task<IActionResult> EditPhraseAsync(string id, [FromBody] PhraseDto phrase)
        {
            try
            {
                var (isSuccessful, message) = await _runtime.ExecuteCommandAsync(new EditPhraseCommand { Id = id, Phrase = phrase });

                if (isSuccessful)
                {
                    return Ok(phrase);
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