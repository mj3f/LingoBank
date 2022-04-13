namespace LingoBank.Core;

public record RuntimeCommandResult(bool IsSuccessful, string? Message = null);