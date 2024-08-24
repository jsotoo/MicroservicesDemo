using Microservices.Transfer.Service.Infrastructure.Data;
using Microservices.Transfer.Service.Application.Interfaces;
using Microservices.Transfer.Service.Application.UseCases;

namespace Microservices.Transfer.Service.Application.Extensions
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IPerformTransferUseCase, PerformTransferUseCase>();

            return services;
        }
    }    
}
