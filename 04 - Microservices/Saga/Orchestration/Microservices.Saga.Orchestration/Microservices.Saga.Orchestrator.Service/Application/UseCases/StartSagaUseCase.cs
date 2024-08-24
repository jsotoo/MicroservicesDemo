
using Microservices.Infrastructure.Kafka.Producer;
using Microservices.Messages.Enums;
using Microservices.Messages.Service.Orchestrator.Events;
using Microservices.Saga.Orchestrator.Service.Application.Enums;
using Microservices.Saga.Orchestrator.Service.Application.Interfaces;
using Microservices.Saga.Orchestrator.Service.Core.Results;
using Microservices.Saga.Orchestrator.Service.Domain.Interfaces;

namespace Microservices.Saga.Orchestrator.Service.Application.UseCases
{
    public class StartSagaUseCase : IStartSagaUseCase
    {
        private readonly IMessageProducer _messageProducer;
        private readonly ILogger<StartSagaUseCase> _logger;
        private readonly IOrchestratorUnitOfWork _unitOfWork;

        public StartSagaUseCase(
            IMessageProducer messageProducer,
            ILogger<StartSagaUseCase> logger,
            IOrchestratorUnitOfWork unitOfWork
        )
        {
            _messageProducer = messageProducer;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<StartSagaResult> ExecuteAsync(Domain.Entities.Saga saga)
        {
            var transactionId = Guid.NewGuid();
            try
            {
                var newSaga = await _unitOfWork.SagaRepository.CreateAsync(new Domain.Entities.Saga(
                    transactionId,
                    SagaState.PENDING,
                    WorkflowState.StartSaga,
                    saga.AccountId,
                    saga.Amount,
                    saga.TransferType
                    ));

                await _unitOfWork.CommitAsync();

                await _messageProducer.SendMessageAsync(new SagaStartedEvent(transactionId, newSaga.AccountId,newSaga.Amount, newSaga.TransferType));
                return new StartSagaResult(true,newSaga.TransactionId);
            }
            catch (Exception ex)
            {                
                return new StartSagaResult(false, transactionId, "Unexpected error occurred");
            }
        }
    }
}
