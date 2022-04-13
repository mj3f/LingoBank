using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace LingoBank.API.Authentication
{
    public static class JwtTokenGenerationOptions
    {
        public const string Issuer = "lingobank.api/jwt/issuer";
        public const string Audience = "lingobank.api/jwt/audience";
        public const string Subject = "user";
        public const string AppSettingsJwtKeyIndex = "JwtSecretKey";
        public const int TokenExpiryInMinutes = 30;
    }
}