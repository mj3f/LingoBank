using System;
using System.ComponentModel;
using System.Threading.Tasks;
using LingoBank.Core;
using LingoBank.Core.Commands;
using LingoBank.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LingoBank.API.Controllers
{
    [Route("api/v0/phrases")]
    [Produces("application/json")]
    [ApiController]
    public sealed class PhrasesController : ControllerBase
    {
        private readonly IRuntime _runtime;

        public PhrasesController(IRuntime runtime) => _runtime = runtime;
        
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Description("Creates a new phrase for a language")]
        public async Task<IActionResult> CreatePhraseAsync([FromBody] PhraseDto phrase)
        {
            try
            {
                await _runtime.ExecuteCommandAsync(new CreatePhraseCommand { Phrase = phrase });
                return Ok("Phrase created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Description("Edits an existing phrase")]
        public async Task<IActionResult> EditPhraseAsync(string id, [FromBody] PhraseDto phrase)
        {
            try
            {
                await _runtime.ExecuteCommandAsync(new EditPhraseCommand { Id = id, Phrase = phrase });
                return Ok("Phrase Modified");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}