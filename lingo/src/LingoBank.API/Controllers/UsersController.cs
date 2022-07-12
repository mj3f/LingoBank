using System;
using System.Collections.Generic;
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
    [Route("api/v0/users")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{Roles.Administrator}, {Roles.User}")]
    public sealed class UsersController : ControllerBase
    {
        private readonly IRuntime _runtime;

        public UsersController(IRuntime runtime) => _runtime = runtime;

        [HttpGet]
        [AllowAnonymous]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(typeof(List<UserDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Description("Returns a list of all users. This task can be performed by an administrator only.")]
        public async Task<IActionResult> GetAllAsync()
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
        public async Task<IActionResult> GetByIdAsync(string id)
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

        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Description("Creates a new user.")]
        public async Task<IActionResult> CreateAsync(CreateUserDto createUser)
        {
            try
            {
                await _runtime.ExecuteCommandAsync(new CreateUserCommand { CreateUser = createUser, Role = Roles.User });
                
                return Ok("User created.");
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
        [Description("Edits an existing user.")]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] UserDto user)
        {
            try
            {
                await _runtime.ExecuteCommandAsync(new EditUserCommand { User = user });

                return Ok("User updated.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Description("Deletes an existing user.")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                await _runtime.ExecuteCommandAsync(new DeleteUserCommand { Id = id });

                return Ok("User deleted.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/password-reset-token")]
        [AllowAnonymous]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Description("Returns a password reset token for a given user." +
                     "This endpoint is currently accessible only to Admins")] // TODO: Implement reset token verification for regular users.
        public async Task<IActionResult> RequestPasswordResetTokenAsync([FromRoute] string id)
        {
            try
            {
                string token = await _runtime.ExecuteQueryAsync(new GetPasswordResetTokenQuery
                {
                    UserId = id
                });

                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/reset-password")]
        [AllowAnonymous]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Description("Resets a users password." +
                     "This endpoint is currently accessible only to Admins")] // TODO: Implement reset token verification for regular users.
        public async Task<IActionResult> ResetUserPasswordAsync([FromRoute] string id,
            [FromBody] ResetUserPasswordDto resetUserPasswordDto)
        {
            try
            {
                await _runtime.ExecuteCommandAsync(new ResetUserPasswordCommand
                {
                    UserId = id,
                    ResetToken = resetUserPasswordDto.ResetToken,
                    NewPassword = resetUserPasswordDto.NewPassword
                });

                return Ok("Password has been reset.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}