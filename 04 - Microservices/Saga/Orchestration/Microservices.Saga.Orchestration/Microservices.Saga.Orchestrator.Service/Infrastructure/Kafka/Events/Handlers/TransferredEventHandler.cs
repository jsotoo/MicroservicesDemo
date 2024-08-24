using MediatR;
using Microservices.Messages.Service.Receipt.Events;
using Microservices.Messages.Service.Transfer.Events;
using Microservices.Saga.Orchestrator.Service.Application.Enums;
using Microservices.Saga.Orchestrator.Service.Application.Interfaces;

namespace Microservices.Saga.Orchestrator.Service.Infrastructure.Kafka.Events.Handlers
{
    public class TransferredEventHandler : IRequestHandler<TransferredEvent>
    {
        private readonly IWorkflowSagaUseCase _workflowSagaUseCase;

        public TransferredEventHandler(IWorkflowSagaUseCase workflowSagaUseCase)
        {
            _workflowSagaUseCase = workflowSagaUseCase;
        }
        public async Task Handle(TransferredEvent request, CancellationToken cancellationToken)
        {
            var saga = new Domain.Entities.Saga(request.TransactionId, WorkflowState.Transferred);
            await _workflowSagaUseCase.ExecuteAsync(saga);
        }
    }
}
