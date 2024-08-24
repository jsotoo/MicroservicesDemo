using MediatR;
using Microservices.Messages.Service.Receipt.Events;
using Microservices.Saga.Orchestrator.Service.Application.Enums;
using Microservices.Saga.Orchestrator.Service.Application.Interfaces;

namespace Microservices.Saga.Orchestrator.Service.Infrastructure.Kafka.Events.Handlers
{
    public class ReceiptIssuedEventHandler : IRequestHandler<ReceiptIssuedEvent>
    {
        private readonly IWorkflowSagaUseCase _workflowSagaUseCase;

        public ReceiptIssuedEventHandler(IWorkflowSagaUseCase workflowSagaUseCase)
        {
            _workflowSagaUseCase = workflowSagaUseCase;
        }
        public async Task Handle(ReceiptIssuedEvent request, CancellationToken cancellationToken)
        {
            var saga = new Domain.Entities.Saga(request.TransactionId, WorkflowState.ReceiptIssued);
            await _workflowSagaUseCase.ExecuteAsync(saga);
        }
    }
}
