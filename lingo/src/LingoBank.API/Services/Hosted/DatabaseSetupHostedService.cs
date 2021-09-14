using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LingoBank.Core;
using LingoBank.Core.Commands;
using LingoBank.Core.Dtos;
using LingoBank.Core.Queries;
using LingoBank.Database.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace LingoBank.API.Services.Hosted
{
    public sealed class DatabaseSetupHostedService : IHostedService
    {
        private static readonly ILogger Logger = Log.ForContext<DatabaseSetupHostedService>();
        private readonly IServiceProvider _serviceProvider;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IdentityResultHandlerLoggingService _identityResultHandlerLoggingService;

        public DatabaseSetupHostedService(IServiceProvider serviceProvider, IWebHostEnvironment env, IdentityResultHandlerLoggingService loggingService)
        {
            _serviceProvider = serviceProvider;
            _hostingEnvironment = env;
            _identityResultHandlerLoggingService = loggingService;
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await CreateDatabase(cancellationToken);
            await SeedDefaultDevelopmentAdminAccount();
        }

        /// <summary>
        /// Ensure the database is created.
        /// </summary>
        private async Task CreateDatabase(CancellationToken cancellationToken)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    LingoContext lingoContext = scope.ServiceProvider.GetRequiredService<LingoContext>();
                    IEnumerable<string> pendingMigrations = await lingoContext.Database.GetAppliedMigrationsAsync(cancellationToken);
                    
                    if (pendingMigrations.Any())
                    {
                        await lingoContext.Database.MigrateAsync(cancellationToken);
                    }
                    else
                    {
                        await lingoContext.Database.EnsureCreatedAsync(cancellationToken);
                    }
                    
                    Logger.Information("[DatabaseSetupHostedService] Database created.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"[STARTUP] An error occurred whilst migrating the database (perhaps it has already been created?)." +
                              $"See exception message for details. {ex.Message}");
            }
        }

        private async Task SeedDefaultDevelopmentAdminAccount()
        {
            if (!_hostingEnvironment.IsDevelopment())
            {
                return;
            }
            
            using (var scope = _serviceProvider.CreateScope())
            {
                var runtime = scope.ServiceProvider.GetRequiredService<IRuntime>();

                try
                {
                    UserDto existingUser = await runtime.ExecuteQueryAsync(new GetUserByIdQuery
                    {
                        EmailAddress = "admin@example.com"
                    });

                    if (existingUser is not null)
                    {
                        Logger.Information("[DatabaseSetupHostedService] development admin account has already been created, skipping this...");
                        return;
                    }
                    
                    await runtime.ExecuteCommandAsync(new CreateUserCommand
                    {
                        UserWithPassword = new UserWithPasswordDto
                        {
                            EmailAddress = "admin@example.com",
                            Role = "Administrator",
                            UserName = "devadmin",
                            Password = "HelloWorld12345!"
                        },
                        HandleResult = result => _identityResultHandlerLoggingService.LogIdentityResult(result, 
                            "[DatabaseSetupHostedService] development admin account has been created.",
                            "[DatabaseSetupHostedService] error occurred whilst creating admin user.")
                    });
                }
                catch (Exception ex)
                {
                    Logger.Error($"[DatabaseSetupHostedService] could not seed the development admin account. " +
                                 $"See exception message for details: {ex.Message} ");
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}