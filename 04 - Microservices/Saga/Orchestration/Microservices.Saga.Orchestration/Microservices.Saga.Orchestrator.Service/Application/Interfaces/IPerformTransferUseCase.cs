using Microservices.Saga.Orchestrator.Service.Core.Results;
using Microservices.Saga.Orchestrator.Service.Domain.Entities;

namespace Microservices.Saga.Orchestrator.Service.Application.Interfaces
{
    public interface IPerformTransferUseCase
    {
        Task<PerformTransferResult> ExecuteAsync(Domain.Entities.Saga saga);
    }

}
