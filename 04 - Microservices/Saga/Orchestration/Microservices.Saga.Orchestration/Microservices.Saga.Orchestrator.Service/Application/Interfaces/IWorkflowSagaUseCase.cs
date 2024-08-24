using Microservices.Messages.Enums;
using Microservices.Saga.Orchestrator.Service.Application.Enums;
using Microservices.Saga.Orchestrator.Service.Core.Results;

namespace Microservices.Saga.Orchestrator.Service.Application.Interfaces
{
    public interface IWorkflowSagaUseCase
    {
        Task<WorkflowResult> ExecuteAsync(Domain.Entities.Saga saga);
    }
}