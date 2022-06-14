using System.Collections.Generic;
using LingoBank.Core.Dtos;

namespace LingoBank.Core.Queries
{
    /// <summary>
    /// Returns a list of all users in the system. This query is accessible from the UsersController, and can only be
    /// called by users with the Admin role.
    /// </summary>
    public sealed class GetUsersQuery : IRuntimeQuery<List<UserDto>>
    {
        public bool Validate() => true;
    }
}