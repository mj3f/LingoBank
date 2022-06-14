namespace LingoBank.Core.Dtos;

/// <summary>
/// User inputs a username, email address and password when creating a user account.
/// </summary>
/// <param name="UserName"></param>
/// <param name="EmailAddress"></param>
/// <param name="Password"></param>
public sealed record CreateUserDto(string UserName, string EmailAddress, string Password);