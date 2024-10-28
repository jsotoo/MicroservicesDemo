using Microservices.Demo.Report.API.Infrastructure.Agents.Product;
using Microservices.Demo.Report.API.Infrastructure.Configuration;

namespace Microservices.Demo.Report.API.Infrastructure.Agents
{
    public static class Extensions
    {
        public static IServiceCollection AddRestClients(this IServiceCollection services, IConfiguration configuration)
        {
            // Registrar la configuración
            services.Configure<ServicesUrl>(
            configuration.GetSection("ServicesUrl"));

            services.AddHttpClient();

            services.AddSingleton<IProductClient, ProductClient>();
            services.AddSingleton<IProductAgent, ProductAgent>();

            return services;
        }
    }
}
