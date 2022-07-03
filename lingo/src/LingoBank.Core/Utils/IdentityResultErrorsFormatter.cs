using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace LingoBank.Core.Utils;

/// <summary>
/// Static class that takes an <see cref="IEnumerable{IdentityError}"/> and returns a formatted string for the API
/// endpoint to return in the case of ASP.NET Identity returning an error.
/// </summary>
public static class IdentityResultErrorsFormatter
{
    /// <summary>
    /// Converts an enumerable of IdentityErrors into a single string that can be return to the user in an API response.
    /// </summary>
    /// <param name="errors">The errors thrown by ASP.NET Identity whilst trying to create, edit or delete a User resource></param>
    /// <returns>a formatted string containing all the errors thrown by the Identity library.</returns>
    public static string GetFormattedErrorMessage(IEnumerable<IdentityError> errors)
    {
        string errorMessage = string.Empty;
        foreach (var error in errors)
        {
            errorMessage += $"Error: {error.Code} - {error.Description}";
        }

        return errorMessage;
    }
}