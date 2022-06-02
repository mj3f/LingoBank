using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
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
    [Route("api/v0/users")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public sealed class UsersController : ControllerBase
    {
        private readonly IRuntime _runtime;

        public UsersController(IRuntime runtime) => _runtime = runtime;

        [HttpGet]
        [ProducesResponseType(typeof(List<UserDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Description("Returns a list of all users. This task can be performed by an administrator only.")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _runtime.ExecuteQueryAsync(new GetUsersQuery());
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [Description("Returns details of a user from the id given.")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var user = await _runtime.ExecuteQueryAsync(new GetUserByIdQuery { Id = id });

                if (user is null)
                {
                    return NotFound("No user found for id provided.");
                }
                
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/languages")]
        [Obsolete("Just use Get user by id instead.")]
        [ProducesResponseType(typeof(List<LanguageDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Description("Returns a list of languages belonging to a user.")]
        public async Task<IActionResult> GetLanguagesAsync(string id)
        {
            try
            {
                UserDto user = await _runtime.ExecuteQueryAsync(new GetUserByIdQuery { Id = id });
                if (user is null)
                {
                    return NotFound("No user found for id provided.");
                }

                List<LanguageDto> languages = await _runtime.ExecuteQueryAsync(new GetLanguagesQuery { UserId = id });
                return Ok(languages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Description("Creates a new user.")]
        public async Task<IActionResult> Create(UserWithPasswordDto userWithPassword)
        {
            try
            {
                var (isSuccessful, message) = await _runtime.ExecuteCommandAsync(new CreateUserCommand { UserWithPassword = userWithPassword });

                if (isSuccessful)
                {
                    return Ok((UserDto) userWithPassword); // TODO: Check this doesn't send the password back.
                }

                return BadRequest(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Description("Edits an existing user.")]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] UserDto user)
        {
            try
            {
                var (isSuccessful, message) = await _runtime.ExecuteCommandAsync(new EditUserCommand { User = user });

                if (isSuccessful)
                {
                    return Ok(user);
                }

                return BadRequest(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Description("Deletes an existing user.")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                var (isSuccessful, message) = await _runtime.ExecuteCommandAsync(new DeleteUserCommand { Id = id });

                if (isSuccessful)
                {
                    return Ok(id);
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