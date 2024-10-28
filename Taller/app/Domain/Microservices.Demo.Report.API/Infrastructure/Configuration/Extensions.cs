using Microservices.Demo.Policy.API.Infrastructure.Configuration;

namespace Microservices.Demo.Report.API.Infrastructure.Configuration
{
    public static class Extensions
    {
        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {

            var servicesUrl = configuration.GetSection("ServicesUrl");
            services.Configure<ServicesUrl>(servicesUrl);

            return services;
        }
    }
}
