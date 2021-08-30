using System.Threading.Tasks;
using LingoBank.Core.Dtos;

namespace LingoBank.API.Services
{
    public interface ITokenService
    {
        Task<string> BuildToken(string email);
        bool ValidateToken(string token);
    }
}