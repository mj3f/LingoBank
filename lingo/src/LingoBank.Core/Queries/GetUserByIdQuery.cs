using LingoBank.Core.Dtos;

namespace LingoBank.Core.Queries
{
    public sealed class GetUserByIdQuery : IRuntimeQuery<UserDto>
    {
        /// <summary>
        /// Search for a user using their unique identifier.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Search for a user using their email address.
        /// </summary>
        public string EmailAddress { get; set; }
        
        /// <summary>
        /// Search for a user using their unique username.
        /// </summary>
        public string Username { get; set; }

        public bool IncludeLanguages { get; set; } = true;
        
        public bool Validate() => !string.IsNullOrEmpty(Id) || !string.IsNullOrEmpty(EmailAddress) || !string.IsNullOrEmpty(Username);
    }
}