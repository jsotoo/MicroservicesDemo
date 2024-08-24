
using MediatR;
using Microservices.Messages.Service.Orchestrator.Events;
using Microservices.Saga.Orchestrator.Service.Application.Enums;
using Microservices.Saga.Orchestrator.Service.Application.Interfaces;

namespace Microservices.Saga.Orchestrator.Service.Infrastructure.Kafka.Events.Handlers
{
    public class SagaStartedEventHandler : IRequestHandler<SagaStartedEvent>
    {
        private readonly IWorkflowSagaUseCase _workflowSagaUseCase;

        public SagaStartedEventHandler(IWorkflowSagaUseCase workflowSagaUseCase)
        {
            _workflowSagaUseCase = workflowSagaUseCase;
        }
        public async Task Handle(SagaStartedEvent request, CancellationToken cancellationToken)
        {
            var saga = new Domain.Entities.Saga(request.TransactionId, WorkflowState.SagaStarted);
            await _workflowSagaUseCase.ExecuteAsync(saga);
        }
    }
}
