using LingoBank.Core.Dtos;
using Microsoft.AspNetCore.Identity;

namespace LingoBank.Core.Queries
{
    /// <summary>
    /// Query used to authenticate a users login credentials, and sign them into the system if the credentials provided
    /// are valid. This returns a <see cref="SignInResult"/> so this is called as a query, rather than as a command.
    /// </summary>
    public sealed class SignInUserQuery : IRuntimeQuery<SignInResult>
    {
        /// <summary>
        /// The user that wishes to sign in to the system.
        /// </summary>
        public UserDto User { get; set; }
        
        /// <summary>
        /// The password provided by the user.
        /// </summary>
        public string Password { get; set; }
        
        public bool Validate() => User != null && !string.IsNullOrEmpty(Password);
    }
}