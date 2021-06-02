using System.Collections.Generic;

namespace LingoBank.Core.Dtos
{
    public class UserDto
    {
        /// <summary>
        /// Users unique identifier
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// The users username.
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// The users email address.
        /// </summary>
        public string EmailAddress { get; set; }
        
        /// <summary>
        /// The users list of created languages.
        /// </summary>
        public List<LanguageDto> Languages { get; set; }
    }

    public sealed class CreateUserDto : UserDto
    {
        /// <summary>
        /// The users defined password when signing up to the service.
        /// </summary>
        public string Password { get; set; }
    }
}