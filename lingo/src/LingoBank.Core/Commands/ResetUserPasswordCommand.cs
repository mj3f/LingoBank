using LingoBank.Core.Dtos;

namespace LingoBank.Core.Commands;

/// <summary>
/// Resets an existing users password.
/// </summary>
public class ResetUserPasswordCommand : IRuntimeCommand
{
    /// <summary>
    /// The users unique identifier.
    /// </summary>
    public string UserId { get; init; }
    
    /// <summary>
    /// Password reset token.
    /// </summary>
    public string ResetToken { get; init; }
    
    /// <summary>
    /// Users new password to change to.
    /// </summary>
    public string NewPassword { get; init; }

    public bool Validate() => !string.IsNullOrEmpty(UserId) &&
                              !string.IsNullOrEmpty(ResetToken) &&
                              !string.IsNullOrEmpty(NewPassword);

    public bool HasExecuted { get; set; }
}