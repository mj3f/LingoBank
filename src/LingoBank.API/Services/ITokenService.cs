using System.Threading.Tasks;
using LingoBank.Core.Dtos;

namespace LingoBank.API.Services
{
    public interface ITokenService
    {
        Task<string> BuildToken(string userName);
        bool ValidateToken(string token);
    }
}