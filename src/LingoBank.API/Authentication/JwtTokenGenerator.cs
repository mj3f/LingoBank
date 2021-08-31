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
        private const double ExpiryInMinutes = 30;
        private static ILogger _logger = Log.ForContext<JwtTokenGenerator>();
        private readonly IRuntime _runtime;
        
        // Properties for generating/validating a JWT token.
        private readonly string _key, _issuer, _audience;

        public JwtTokenGenerator(IConfiguration configuration, IRuntime runtime)
        {
            _key = configuration[JwtTokenGenerationOptions.AppSettingsJwtKeyIndex];
            _issuer = JwtTokenGenerationOptions.Issuer;
            _audience = JwtTokenGenerationOptions.Audience;
            _runtime = runtime;
        }

        public async Task<string> BuildToken(string email)
        {
            if (string.IsNullOrEmpty(_key) || string.IsNullOrEmpty(_audience) || string.IsNullOrEmpty(_issuer))
            {
                _logger.Error("[TOKEN SERVICE] One of the properties required for generating a JWT token was null. Check that Key, Audience & Issuer are set in the configuration.");
                return string.Empty;
            }

            UserDto userDto = await _runtime.ExecuteQueryAsync(new GetUserByIdQuery
                { EmailAddress = email, IncludeLanguages = false });

            if (userDto is null)
            {
                _logger.Error("[TOKEN SERVICE] Could not find a user with username " + email);
                return string.Empty;
            }
            
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userDto.Id),
                // new Claim(ClaimTypes.Name, userDto.UserName),
                new Claim(ClaimTypes.Email, userDto.EmailAddress),
                new Claim(ClaimTypes.Role, userDto.Role),
                // new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            
            var tokenDescriptor = new JwtSecurityToken(
                _issuer, 
                _audience, 
                claims,
                expires: DateTime.Now.AddMinutes(ExpiryInMinutes), 
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)), SecurityAlgorithms.HmacSha256));
            
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public bool ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(_key) || string.IsNullOrEmpty(_audience) || string.IsNullOrEmpty(_issuer))
            {
                _logger.Error("[TOKEN SERVICE] One of the properties required for generating a JWT token was null. Check that Key, Audience & Issuer are set in the configuration.");
                return false;
            }
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _issuer,
                    ValidAudience = _audience,
                    IssuerSigningKey = securityKey
                }, out SecurityToken validatedToken);
            }
            catch (Exception ex)
            {
                _logger.Error("[TOKEN SERVICE] An error occured whilst trying to validate a JWT token. Exception details: " + ex.Message);
                return false;
            }

            return true;
        }
    }
}