using System.Collections.Generic;
using LingoBank.Core.Dtos;

namespace LingoBank.Core.Queries
{
    public sealed class GetUsersQuery : IRuntimeQuery<List<UserDto>>
    {
        public bool Validate() => true;
    }
}