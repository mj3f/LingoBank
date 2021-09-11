using LingoBank.API.Authentication;
using LingoBank.Core;
using Microsoft.Extensions.DependencyInjection;

namespace LingoBank.API.UnitTests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            RuntimeIocContainer.ConfigureServicesForRuntime(services);
        }
    }
}