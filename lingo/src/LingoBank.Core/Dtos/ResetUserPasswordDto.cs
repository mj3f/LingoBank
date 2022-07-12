
namespace LingoBank.Core.Dtos;

/// <summary>
/// Dto to reset a users password, provided a valid user id, reset token and new password provided.
/// </summary>
public sealed record ResetUserPasswordDto
{
    // Populated by fetching the user id from the API request route, rather than in the body.
    public string? UserId { get; init; }
    
    public string ResetToken { get; init; }
    
    public string NewPassword { get; init; }
}