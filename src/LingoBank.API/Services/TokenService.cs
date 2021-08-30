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

namespace LingoBank.API.Services
{
    public class TokenService : ITokenService
    {
        private const double ExpiryInMinutes = 30;
        private static ILogger _logger = Log.ForContext<TokenService>();
        private readonly IRuntime _runtime;
        
        // Properties for generating/validating a JWT token.
        private readonly string _key, _issuer, _audience;

        public TokenService(IConfiguration configuration, IRuntime runtime)
        {
            _key = configuration["Jwt:Key"];
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];
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
                new Claim(ClaimTypes.Name, userDto.UserName),
                new Claim(ClaimTypes.Role, userDto.Role),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));        
            
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);           
            
            var tokenDescriptor = new JwtSecurityToken(_issuer, _issuer, claims,
                expires: DateTime.Now.AddMinutes(ExpiryInMinutes), signingCredentials: credentials);
            
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