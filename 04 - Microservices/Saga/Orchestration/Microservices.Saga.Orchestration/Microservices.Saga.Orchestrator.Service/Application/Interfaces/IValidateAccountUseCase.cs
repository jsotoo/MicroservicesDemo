using Microservices.Saga.Orchestrator.Service.Core.Results;
using Microservices.Saga.Orchestrator.Service.Domain.Entities;

namespace Microservices.Saga.Orchestrator.Service.Application.Interfaces
{
    public interface IValidateAccountUseCase
    {
        Task<ValidateAccountResult> ExecuteAsync(Domain.Entities.Saga saga);
    }

}
