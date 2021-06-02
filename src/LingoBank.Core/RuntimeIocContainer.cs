using System.Collections.Generic;
using LingoBank.Core.CommandHandlers;
using LingoBank.Core.Commands;
using LingoBank.Core.Dtos;
using LingoBank.Core.Queries;
using LingoBank.Core.QueryHandlers;
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

            services.AddTransient<IRuntimeQueryHandler<GetLanguagesQuery, List<LanguageDto>>, GetLanguagesQueryHandler>();
            services.AddTransient<IRuntimeQueryHandler<GetLanguageByIdQuery, LanguageDto>, GetLanguageByIdQueryHandler>();
            services.AddTransient<IRuntimeQueryHandler<GetPhrasesQuery, List<PhraseDto>>, GetPhrasesQueryHandler>();
            services.AddTransient<IRuntimeQueryHandler<GetUsersQuery, List<UserDto>>, GetUsersQueryHandler>();
            services.AddTransient<IRuntimeQueryHandler<GetUserByIdQuery, UserDto>, GetUserByIdQueryHandler>();
            
            #endregion

            #region Commands
            services.AddTransient<IRuntimeCommandHandler<CreateLanguageCommand>, CreateLanguageCommandHandler>();
            services.AddTransient<IRuntimeCommandHandler<EditLanguageCommand>, EditLanguageCommandHandler>();
            services.AddTransient<IRuntimeCommandHandler<CreatePhraseCommand>, CreatePhraseCommandHandler>();
            services.AddTransient<IRuntimeCommandHandler<EditPhraseCommand>, EditPhraseCommandHandler>();
            services.AddTransient<IRuntimeCommandHandler<CreateUserCommand>, CreateUserCommandHandler>();
            services.AddTransient<IRuntimeCommandHandler<EditUserCommand>, EditUserCommandHandler>();
            services.AddTransient<IRuntimeCommandHandler<DeleteUserCommand>, DeleteUserCommandHandler>();
            #endregion
        }
    }
}