using MediatR;
using Microservices.Messages.Service.Validator.Events;
using Microservices.Saga.Orchestrator.Service.Application.Enums;
using Microservices.Saga.Orchestrator.Service.Application.Interfaces;

namespace Microservices.Saga.Orchestrator.Service.Infrastructure.Kafka.Events.Handlers
{
    public class AccountValidatedEventHandler : IRequestHandler<AccountValidatedEvent>
    {
        private readonly IWorkflowSagaUseCase _workflowSagaUseCase;

        public AccountValidatedEventHandler(IWorkflowSagaUseCase workflowSagaUseCase)
        {
            _workflowSagaUseCase = workflowSagaUseCase;
        }
        public async Task Handle(AccountValidatedEvent request, CancellationToken cancellationToken)
        {
            var saga = new Domain.Entities.Saga(request.TransactionId, WorkflowState.AccountValidated);
            await _workflowSagaUseCase.ExecuteAsync(saga);
        }
    }
}
