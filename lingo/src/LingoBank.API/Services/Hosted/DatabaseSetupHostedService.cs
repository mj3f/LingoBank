using System;
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
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;

        public DatabaseSetupHostedService(
            IServiceProvider serviceProvider,
            IWebHostEnvironment env,
            IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _hostingEnvironment = env;
            _configuration = configuration;
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!IsDatabaseServerAlive())
            {
                Logger.Error("[DatabaseSetupHostedService] Database connection could not be established." +
                             "Trying again in 5 seconds...");
                await Task.Delay(5000, cancellationToken);
            }
            Logger.Information("[DatabaseSetupHostedService] Database connection established.");
            
            await CreateDatabase(cancellationToken);
            await SeedDefaultDevelopmentAdminAccount();
        }

        private bool IsDatabaseServerAlive()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                LingoContext lingoContext = scope.ServiceProvider.GetRequiredService<LingoContext>();
                return lingoContext.Database.CanConnect();
            }
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
                string username = "devadmin";
                string emailAddress = "admin@example.com";

                try
                {
                    UserDto existingUser = await runtime.ExecuteQueryAsync(new GetUserByIdQuery
                    {
                        EmailAddress = emailAddress
                    });

                    if (existingUser is not null)
                    {
                        Logger.Information("[DatabaseSetupHostedService] development admin account has already been created, skipping this...");
                        return;
                    }
                    
                    string? password = _configuration.GetSection("DevAdminAccount:Password").Value;

                    if (string.IsNullOrEmpty(password))
                    {
                        Logger.Error("[DatabaseSetupHostedService] Error: Password input for dev account not" +
                                     "provided in appSettings.json.");
                        return;
                    }
                    
                    RuntimeCommandResult result = await runtime.ExecuteCommandAsync(new CreateUserCommand
                    {
                        UserWithPassword = new UserWithPasswordDto
                        {
                            EmailAddress = emailAddress,
                            Role = "Administrator",
                            UserName = username,
                            Password = password
                        }
                    });

                    if (result.IsSuccessful)
                    {
                        Logger.Information("[DatabaseSetupHostedService] Dev admin account seeded.");
                    }
                    else
                    {
                        Logger.Error("[DatabaseSetupHostedService] Dev admin account failed to seed. See exception(s) for details" +
                                           $"{result.Message}");
                    }
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