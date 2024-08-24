using Microservices.Infrastructure.Kafka.Producer;
using Microservices.Messages.Service.Transfer.Commands;
using Microservices.Saga.Orchestrator.Service.Application.Interfaces;
using Microservices.Saga.Orchestrator.Service.Core.Results;
using Microservices.Saga.Orchestrator.Service.Domain.Entities;
using Microservices.Saga.Orchestrator.Service.Domain.Interfaces;

namespace Microservices.Saga.Orchestrator.Service.Application.UseCases
{
    public class PerformTransferUseCase : IPerformTransferUseCase
    {
        private readonly IMessageProducer _messageProducer;
        private readonly ILogger<PerformTransferUseCase> _logger;
        private readonly IOrchestratorUnitOfWork _unitOfWork;

        public PerformTransferUseCase(
            IMessageProducer messageProducer,
            ILogger<PerformTransferUseCase> logger,
            IOrchestratorUnitOfWork unitOfWork
        )
        {
            _messageProducer = messageProducer;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<PerformTransferResult> ExecuteAsync(Domain.Entities.Saga saga)
        {
            try
            {
                await _messageProducer.SendMessageAsync(new PerformTransferCommand(saga.TransactionId, saga.AccountId, saga.Amount, saga.TransferType));
                return new PerformTransferResult(true,saga.TransactionId);
            }
            catch (Exception ex)
            {                
                return new PerformTransferResult(false,saga.TransactionId, ex.Message);
            }
        }
    }
}
