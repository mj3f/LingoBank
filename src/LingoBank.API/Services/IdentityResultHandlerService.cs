using Microsoft.AspNetCore.Identity;
using Serilog;
using Serilog.Core;

namespace LingoBank.API.Services
{
    public sealed class IdentityResultHandlerLoggingService
    {
        private static readonly ILogger Logger = Log.ForContext<IdentityResultHandlerLoggingService>();
        
        /// <summary>
        /// Simply logs any errors without repetition of code.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="successMessage"></param>
        /// <param name="failureMessage"></param>
        public void LogIdentityResult(IdentityResult result, string? successMessage = null, string? failureMessage = null)
        {
            if (result.Succeeded)
            {
                Logger.Information(successMessage ?? "[IdentityResultHandlerLoggingService] Result succeeded.");
            }
            else
            {
                Logger.Information((failureMessage ?? "[IdentityResultHandlerLoggingService] Error occurred during execution.") + 
                                   "See below for more information:");
                foreach (var error in result.Errors)
                {
                    Logger.Error($"[IdentityResultHandlerLoggingService] Error Code: {error.Code}");
                    Logger.Error($"[IdentityResultHandlerLoggingService] Error Description: {error.Description}");
                }
            }
        }

        /// <summary>
        /// Returns true or false depending on whether the identity result succeeded or not.
        /// Allows calling code to handle failure cases.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool IdentityResultHasSucceeded(IdentityResult result) => result.Succeeded;
    }
}