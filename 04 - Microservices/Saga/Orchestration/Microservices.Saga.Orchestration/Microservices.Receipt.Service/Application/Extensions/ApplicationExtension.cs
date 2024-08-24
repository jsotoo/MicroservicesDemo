using Microservices.Receipt.Service.Infrastructure.Persistence;
using Microservices.Receipt.Service.Application.Interfaces;
using Microservices.Receipt.Service.Application.UseCases;

namespace Microservices.Receipt.Service.Application.Extensions
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IIssueReceiptUseCase, IssueReceiptUseCase>();

            return services;
        }
    }    
}
