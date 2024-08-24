using Microservices.Messages.Enums;
using Microservices.Saga.Orchestrator.Service.Core.Results;
using Microservices.Saga.Orchestrator.Service.Domain.Entities;

namespace Microservices.Saga.Orchestrator.Service.Application.Interfaces
{
    public interface IStartSagaUseCase
    {
        Task<StartSagaResult> ExecuteAsync(Domain.Entities.Saga saga);
    }

}
