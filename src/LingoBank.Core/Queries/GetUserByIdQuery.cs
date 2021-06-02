using LingoBank.Core.Dtos;

namespace LingoBank.Core.Queries
{
    public sealed class GetUserByIdQuery : IRuntimeQuery<UserDto>
    {
        public string Id { get; set; }
        public bool Validate() => !string.IsNullOrEmpty(Id);
    }
}