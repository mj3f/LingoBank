using LingoBank.Core.Commands;

namespace LingoBank.Core.Queries;

/// <summary>
/// Returns a password reset token that can be passed into the
/// <see cref="ResetUserPasswordCommand"/>
/// </summary>
public sealed class GetPasswordResetTokenQuery : IRuntimeQuery<string>
{
    public string UserId { get; init; }

    public bool Validate() => !string.IsNullOrEmpty(UserId);
}