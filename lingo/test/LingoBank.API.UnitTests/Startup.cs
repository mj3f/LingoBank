using LingoBank.API.Authentication;
using LingoBank.Core;
using LingoBank.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LingoBank.API.UnitTests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddDbContext<LingoContext>(options => options.UseInMemoryDatabase("Lingo"));
            RuntimeIocContainer.ConfigureServicesForRuntime(services);
        }
    }
}