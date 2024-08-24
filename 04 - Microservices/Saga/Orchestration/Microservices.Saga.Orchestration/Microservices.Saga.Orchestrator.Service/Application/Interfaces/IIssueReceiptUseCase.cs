using Microservices.Saga.Orchestrator.Service.Core.Results;
using Microservices.Saga.Orchestrator.Service.Domain.Entities;

namespace Microservices.Saga.Orchestrator.Service.Application.Interfaces
{
    public interface IIssueReceiptUseCase
    {
        Task<IssueReceiptResult> ExecuteAsync(Domain.Entities.Saga saga);
    }

}
