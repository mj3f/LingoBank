using System;
using System.Reflection;
using System.Text;
using LingoBank.API.Services;
using LingoBank.API.Services.Hosted;
using LingoBank.Core;
using LingoBank.Database.Contexts;
using LingoBank.Database.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using NSwag;
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
                        Name = "MIT",
                        Url = "https://mit-license.org/"
                    };
                    document.Info.Title = Assembly.GetEntryAssembly().GetName().Name;
                    document.Info.Description = "LingoBank Public API Specification";
                    document.Info.Version = "v" + Assembly.GetEntryAssembly().GetName().Version.ToString();
                };
                config.GenerateAbstractProperties = true;
                config.GenerateKnownTypes = true;
                config.DocumentName = "OpenAPI";
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
                        ServerVersion.FromString("10.4.18-mariadb"),
                        mySqlOptions => mySqlOptions
                            .CharSetBehavior(CharSetBehavior.NeverAppend))
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
            services.AddAuthorization();
            services.AddScoped<ITokenService, TokenService>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            services.AddHostedService<DatabaseSetupHostedService>();
            
            RuntimeIocContainer.ConfigureServicesForRuntime(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            // app.UseHttpsRedirection();

            app.UseCors("AllowOrigin");

            // Leverage session state and add the jwt token to the auth header if applicable.
            app.UseSession();
            app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                
                await next();
            });

            app.UseRouting();
            app.UseStaticFiles();
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints => 
            { 
                endpoints.MapControllers(); 
                endpoints.MapHealthChecks("/health");
            });

            app.MapWhen(x => !x.Request.Path.Value.StartsWith("/swagger"), builder =>
            {
                builder.UseSpa(spa => spa.Options.DefaultPage = "/index.html");
            });
        }
    }
}