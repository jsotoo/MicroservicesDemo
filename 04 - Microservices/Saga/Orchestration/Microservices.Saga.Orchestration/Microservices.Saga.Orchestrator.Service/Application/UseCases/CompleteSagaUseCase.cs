
using Microservices.Infrastructure.Kafka.Producer;
using Microservices.Messages.Service.Orchestrator.Events;
using Microservices.Saga.Orchestrator.Service.Application.Enums;
using Microservices.Saga.Orchestrator.Service.Application.Interfaces;
using Microservices.Saga.Orchestrator.Service.Core.Results;
using Microservices.Saga.Orchestrator.Service.Domain.Interfaces;

namespace Microservices.Saga.Orchestrator.Service.Application.UseCases
{
    public class CompleteSagaUseCase : ICompleteSagaUseCase
    {
        private readonly IMessageProducer _messageProducer;
        private readonly ILogger<CompleteSagaUseCase> _logger;
        private readonly IOrchestratorUnitOfWork _unitOfWork;

        public CompleteSagaUseCase(
            IMessageProducer messageProducer,
            ILogger<CompleteSagaUseCase> logger,
            IOrchestratorUnitOfWork unitOfWork
        )
        {
            _messageProducer = messageProducer;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<CompleteSagaResult> ExecuteAsync(Domain.Entities.Saga saga)
        {            
            try
            {
                saga.Completed();
                saga = await _unitOfWork.SagaRepository.UpdateAsync(saga);
                
                await _messageProducer.SendMessageAsync(new SagaCompletedEvent(saga.TransactionId,saga.AccountId,saga.Amount));
                return new CompleteSagaResult(true,saga.TransactionId);
            }
            catch (Exception ex)
            {                
                return new CompleteSagaResult(false, saga.TransactionId, "Unexpected error occurred");
            }
        }
    }
}
