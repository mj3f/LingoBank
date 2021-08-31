using System.Threading.Tasks;
using LingoBank.Core.Dtos;

namespace LingoBank.API.Authentication
{
    public interface IJwtTokenGenerator
    {
        Task<string> BuildToken(string email);
        bool ValidateToken(string token);
    }
}