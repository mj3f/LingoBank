using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LingoBank.Core;
using LingoBank.Core.Dtos;
using LingoBank.Core.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace LingoBank.API.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private static ILogger _logger = Log.ForContext<JwtTokenGenerator>();
        private readonly IRuntime _runtime;
        
        // Properties for generating/validating a JWT token.
        private readonly string _key;

        public JwtTokenGenerator(IConfiguration configuration, IRuntime runtime)
        {
            _key = configuration[JwtTokenGenerationOptions.AppSettingsJwtKeyIndex];
            _runtime = runtime;
        }

        public async Task<string> BuildToken(string email)
        {
            if (string.IsNullOrEmpty(_key))
            {
                _logger.Error("[TOKEN SERVICE] One of the properties required for generating a JWT token was null. Check that Key, Audience & Issuer are set in the configuration.");
                return string.Empty;
            }

            UserDto? userDto = await _runtime.ExecuteQueryAsync(new GetUserByIdQuery
                { EmailAddress = email });

            if (userDto is null)
            {
                _logger.Error("[JwtTokenGenerator] Could not find a user with username " + email);
                return string.Empty;
            }
            
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userDto.Id),
                new Claim(ClaimTypes.Email, userDto.EmailAddress),
                new Claim(ClaimTypes.Role, userDto.Role),
            };
            
            var tokenDescriptor = new JwtSecurityToken(
                JwtTokenGenerationOptions.Issuer, 
                JwtTokenGenerationOptions.Audience, 
                claims,
                expires: DateTime.Now.AddMinutes(JwtTokenGenerationOptions.TokenExpiryInMinutes), 
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)), SecurityAlgorithms.HmacSha256));
            
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}