using System.Threading.Tasks;
using LingoBank.Core.Queries;
using LingoBank.Database.Entities;
using Microsoft.AspNetCore.Identity;

namespace LingoBank.Core.QueryHandlers
{
    public class SignInUserQueryHandler :  IRuntimeQueryHandler<SignInUserQuery, SignInResult>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        
        public SignInUserQueryHandler(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<SignInResult> ExecuteAsync(SignInUserQuery query)
        {
            // For whatever reason, it works off username rather than email.
            return await _signInManager.PasswordSignInAsync(query.User.UserName, query.Password, false, false);
        }
    }
}