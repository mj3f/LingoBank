using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace LingoBank.API.Authentication
{
    public sealed class JwtTokenGenerationOptions
    {
        public const string Issuer = "lingobank.api/jwt/issuer";
        public const string Audience = "lingobank.api/jwt/audience";
        public const string Subject = "user";
        public const string AppSettingsJwtKeyIndex = "Jwt:Key";
        public const int TokenExpiryInMinutes = 30;
    }
}