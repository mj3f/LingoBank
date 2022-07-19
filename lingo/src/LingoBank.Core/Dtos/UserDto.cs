using System.Collections.Generic;

namespace LingoBank.Core.Dtos
{
    public sealed record UserDto
    {
        /// <summary>
        /// Users unique identifier
        /// </summary>
        public string Id { get; init; } = default!;
        
        /// <summary>
        /// The users username.
        /// </summary>
        public string UserName { get; init; } = default!;
        
        /// <summary>
        /// The users email address.
        /// </summary>
        public string EmailAddress { get; init; } = default!;
        
        /// <summary>
        /// The users system role.
        /// </summary>
        public string Role { get; init; } = default!;
        
        /// <summary>
        /// The users list of created languages.
        /// </summary>
        public Paged<LanguageDto> Languages { get; set; } = null!;
    }
}