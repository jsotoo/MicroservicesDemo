using Microservices.Validator.Service.Infrastructure.Data;
using Microservices.Validator.Service.Application.Interfaces;
using Microservices.Validator.Service.Application.UseCases;

namespace Microservices.Validator.Service.Application.Extensions
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IValidateAccountUseCase, ValidateAccountUseCase>();

            return services;
        }
    }    
}
