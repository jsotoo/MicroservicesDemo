using MediatR;
using Microservices.Messages.Service.Orchestrator.Commands;
using Microservices.Saga.Orchestrator.Service.Application.Enums;
using Microservices.Saga.Orchestrator.Service.Application.Interfaces;

namespace Microservices.Saga.Orchestrator.Service.Infrastructure.Kafka.Events.Handlers
{
    public class StartSagaCommandHandler : IRequestHandler<StartSagaCommand>
    {
        private readonly IWorkflowSagaUseCase _workflowSagaUseCase;

        public StartSagaCommandHandler(IWorkflowSagaUseCase workflowSagaUseCase)
        {
            _workflowSagaUseCase = workflowSagaUseCase;
        }
        public async Task Handle(StartSagaCommand request, CancellationToken cancellationToken)
        {            
            var saga = new Domain.Entities.Saga(request.AccountId,request.Amount,request.Type,WorkflowState.StartSaga);
            await _workflowSagaUseCase.ExecuteAsync(saga);
        }
    }
}
