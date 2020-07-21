using LingoBank.Core.CommandHandlers;
using LingoBank.Core.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace LingoBank.Core
{
    /// <summary>
    /// Container class used to inject queries and commands that are required to perform actions at runtime.
    /// </summary>
    public static class RuntimeIocContainer
    {
        /// <summary>
        /// This method takes the service collection from API Startup as a parameter, and then uses it to inject
        /// the commands and queries, as well as some Core library specific extensions that can also be used by the API.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureServicesForRuntime(IServiceCollection services)
        {
            services.AddScoped<IRuntime, Runtime>();
            
            #region Queries
            
            
            #endregion

            #region Commands
            services.AddTransient<IRuntimeCommandHandler<CreateLanguageCommand>, CreateLanguageCommandHandler>();
            
            #endregion
        }
    }
}