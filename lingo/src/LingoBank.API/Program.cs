using System;
using Azure.Identity;
using LingoBank.Core.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace LingoBank.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // https://github.com/serilog/serilog-aspnetcore
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
            
            try
            {
                Log.Information("Starting Web Host...");
                CreateHostBuilder(args)
                    .Build()
                    .Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, string.Empty);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((context, config) =>
                    {
                        config
                            .SetBasePath(context.HostingEnvironment.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json",
                                optional: true)
                            .AddEnvironmentVariables();

                        var settings = config.Build();
                        string? appConfigurationConnectionString = settings.GetConnectionString("AppConfigurationEndpoint");

                        if (string.IsNullOrEmpty(appConfigurationConnectionString))
                        {
                            Log.Fatal("No azure app configuration endpoint provided in appsettings.json");
                        }
                        else
                        {
                            if (context.HostingEnvironment.IsDevelopment())
                            {
                                var credentials = new DefaultAzureCredential();
                                config.AddAzureAppConfiguration(options =>
                                {
                                    var appConfigOptions = options.Connect(appConfigurationConnectionString)
                                        .Select(KeyFilter.Any, null)
                                        .ConfigureKeyVault(keyVault => keyVault.SetCredential(credentials));

                                    string connectedOrNotConnected =
                                        appConfigOptions != null ? "Connected" : "Could not connect";
                                    Log.Information($"{connectedOrNotConnected} to Azure App Configuration");

                                });
                            }
                            else
                            {
                                var managedIdentityClientId = settings.GetConnectionString("ManagedIdentityClientId");
                                var credentials = new ManagedIdentityCredential(managedIdentityClientId);
                                config.AddAzureAppConfiguration(options =>
                                {
                                    var appConfigOptions = options.Connect(new Uri(appConfigurationConnectionString), credentials)
                                        .Select(KeyFilter.Any, null)
                                        .ConfigureKeyVault(keyVault => keyVault.SetCredential(credentials));
                                    
                                    string connectedOrNotConnected =
                                        appConfigOptions != null ? "Connected" : "Could not connect";
                                    Log.Information($"{connectedOrNotConnected} to Azure App Configuration");
                                });
                            }
                        }
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}