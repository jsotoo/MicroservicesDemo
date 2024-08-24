using Microservices.Saga.Orchestrator.Service.Application.Interfaces;
using Microservices.Saga.Orchestrator.Service.Application.UseCases;

namespace Microservices.Saga.Orchestrator.Service.Application.Extensions
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IStartSagaUseCase, StartSagaUseCase>();
            services.AddTransient<IValidateAccountUseCase, ValidateAccountUseCase>();
            services.AddTransient<IPerformTransferUseCase, PerformTransferUseCase>();
            services.AddTransient<IssueReceiptUseCase, IssueReceiptUseCase>();
            services.AddTransient<ICompleteSagaUseCase, CompleteSagaUseCase>();
            services.AddTransient<IWorkflowSagaUseCase, WorkflowSagaUseCase>();            

            return services;
        }
    }
}
