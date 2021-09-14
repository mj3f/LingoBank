using System.Threading.Tasks;
using LingoBank.Core.Dtos;

namespace LingoBank.API.Authentication
{
    public interface IJwtTokenGenerator
    {
        /// <summary>
        /// Generates an expiring jwt token for a user based on their unique email address.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<string> BuildToken(string email);
    }
}