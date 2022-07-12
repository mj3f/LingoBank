namespace LingoBank.Core.Dtos;

/// <summary>
/// Dto object used by the user to send single string values through a HTTP PUT/POST request.
/// </summary>
public sealed record SingleValueDto(string Value);