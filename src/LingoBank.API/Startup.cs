using System;
using LingoBank.Database.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            services.AddControllers();
            services.AddHealthChecks();
            services.AddDbContext<LingoContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));
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