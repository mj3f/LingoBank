using System;
using System.Reflection;
using System.Text;
using LingoBank.API.Authentication;
using LingoBank.API.Services;
using LingoBank.API.Services.Hosted;
using LingoBank.Core;
using LingoBank.Database.Contexts;
using LingoBank.Database.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using NSwag;
using NSwag.Generation.Processors.Security;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
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
            services.AddSpaStaticFiles(x => x.RootPath = "wwwroot");
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
                    options.SerializerSettings.TypeNameAssemblyFormatHandling =
                        Newtonsoft.Json.TypeNameAssemblyFormatHandling.Simple;
                });
            
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
                        Name = "MIT",
                        Url = "https://mit-license.org/"
                    };
                    document.Info.Title = Assembly.GetEntryAssembly()?.GetName().Name;
                    document.Info.Description = "LingoBank Public API Specification";
                    document.Info.Version = "v" + Assembly.GetEntryAssembly()?.GetName().Version?.ToString();
                };
                config.GenerateAbstractProperties = true;
                config.GenerateKnownTypes = true;
                config.DocumentName = "OpenAPI";
                config.DocumentProcessors.Add(new SecurityDefinitionAppender(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    Description = "Copy 'Bearer ' + valid JWT token into field",
                    In = OpenApiSecurityApiKeyLocation.Header,
                }));
                config.OperationProcessors.Add(new OperationSecurityScopeProcessor(JwtBearerDefaults.AuthenticationScheme));
            });
            
            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: "AllowOrigin",
                    builder =>{
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            services.AddDbContext<LingoContext>(options =>
                options.UseMySql(
                        Configuration.GetConnectionString("DbConnection"),
                        new MariaDbServerVersion("10.4.18-mariadb"))
                        // mySqlOptions => mySqlOptions
                            //.CharSetBehavior(CharSetBehavior.NeverAppend))
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors());

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 8;
                    options.Password.RequiredUniqueChars = 0;

                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;

                    options.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                    options.User.RequireUniqueEmail = true;
                })
                .AddRoles<IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<LingoContext>();

            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddHealthChecks();

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false; // dev envionrment purposes.
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = JwtTokenGenerationOptions.Issuer,
                    ValidAudience = JwtTokenGenerationOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration[JwtTokenGenerationOptions.AppSettingsJwtKeyIndex])),
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddAuthorization();
            services.AddHostedService<DatabaseSetupHostedService>();

            services.AddSingleton<IdentityResultHandlerLoggingService>();
            
            RuntimeIocContainer.ConfigureServicesForRuntime(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
                app.UseSwaggerUi3();
            }

            // app.UseHttpsRedirection();

            app.UseCors("AllowOrigin");

            // Leverage session state and add the jwt token to the auth header if applicable.
            app.UseSession();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseOpenApi();
            
            app.UseEndpoints(endpoints => 
            { 
                endpoints.MapControllers(); 
                endpoints.MapHealthChecks("/health");
            });

            app.MapWhen(x => x.Request.Path.Value != null && !x.Request.Path.Value.StartsWith("/swagger"), builder =>
            {
                builder.UseSpa(spa => spa.Options.DefaultPage = "/index.html");
            });
        }
    }
}