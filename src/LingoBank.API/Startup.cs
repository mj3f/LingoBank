using System;
using System.Reflection;
using LingoBank.Core;
using LingoBank.Database.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using NSwag;
using NSwag.Generation.Processors.Security;
using Serilog;

namespace LingoBank.API
{
    public class Startup
    {
        private static ILogger _logger = Log.ForContext<Startup>();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy
                        {
                            OverrideSpecifiedNames = false
                        }
                    };
                    options.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
                    options.SerializerSettings.TypeNameAssemblyFormatHandling = Newtonsoft.Json.TypeNameAssemblyFormatHandling.Simple;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            
            // Swagger docs
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new OpenApiContact
                    {
                        Name = "LingoBank",
                        Email = "none",
                        Url = "no"
                    };
                    document.Info.License = new OpenApiLicense
                    {
                        Name = "Unlicense",
                        Url = "https://unlicense.org/"
                    };
                    document.Info.Title = Assembly.GetEntryAssembly().GetName().Name;
                    document.Info.Description = "LingoBank Public API Specification";
                    document.Info.Version = "v" + Assembly.GetEntryAssembly().GetName().Version.ToString();
                };
                config.GenerateAbstractProperties = true;
                config.GenerateKnownTypes = true;
                config.DocumentName = "OpenAPI";
            });
            
            services.AddCors();
            
            services.AddDbContext<LingoContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));
            
            services.AddHealthChecks();
            RuntimeIocContainer.ConfigureServicesForRuntime(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, LingoContext lingoContext)
        {
            try
            {
                lingoContext.Database.EnsureDeleted();
                lingoContext.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                _logger.Error($"[STARTUP] An error occurred whilst migrating the database." +
                              $"See exception message for details. {ex.Message}");
            }
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            // app.UseHealthChecks();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}