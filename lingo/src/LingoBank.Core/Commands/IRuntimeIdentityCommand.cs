using System;
using Microsoft.AspNetCore.Identity;

namespace LingoBank.Core.Commands
{
    public interface IRuntimeIdentityCommand : IRuntimeCommand
    {
        Action<IdentityResult> HandleResult { get; set; }
    }
}