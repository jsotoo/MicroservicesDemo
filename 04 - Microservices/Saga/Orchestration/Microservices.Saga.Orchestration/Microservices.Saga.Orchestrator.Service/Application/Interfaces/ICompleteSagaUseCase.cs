using Microservices.Saga.Orchestrator.Service.Core.Results;

namespace Microservices.Saga.Orchestrator.Service.Application.Interfaces
{
    public interface ICompleteSagaUseCase
    {
        Task<CompleteSagaResult> ExecuteAsync(Domain.Entities.Saga saga);
    }

}
