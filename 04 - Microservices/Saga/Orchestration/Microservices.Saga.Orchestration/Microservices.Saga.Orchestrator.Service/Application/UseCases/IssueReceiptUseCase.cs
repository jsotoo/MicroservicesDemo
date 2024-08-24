using Microservices.Infrastructure.Kafka.Producer;
using Microservices.Messages.Service.Receipt.Commands;
using Microservices.Saga.Orchestrator.Service.Application.Interfaces;
using Microservices.Saga.Orchestrator.Service.Core.Results;
using Microservices.Saga.Orchestrator.Service.Domain.Entities;
using Microservices.Saga.Orchestrator.Service.Domain.Interfaces;

namespace Microservices.Saga.Orchestrator.Service.Application.UseCases
{
    public class IssueReceiptUseCase : IIssueReceiptUseCase
    {
        private readonly IMessageProducer _messageProducer;
        private readonly ILogger<IssueReceiptUseCase> _logger;
        private readonly IOrchestratorUnitOfWork _unitOfWork;

        public IssueReceiptUseCase(
            IMessageProducer messageProducer,
            ILogger<IssueReceiptUseCase> logger,
            IOrchestratorUnitOfWork unitOfWork
        )
        {
            _messageProducer = messageProducer;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IssueReceiptResult> ExecuteAsync(Domain.Entities.Saga saga)
        {
            try
            {
                await _messageProducer.SendMessageAsync(new IssueReceiptCommand(saga.TransactionId, saga.AccountId, saga.Amount, saga.TransferType));
                return new IssueReceiptResult(true,saga.TransactionId);
            }
            catch (Exception ex)
            {                
                return new IssueReceiptResult(false,saga.TransactionId, ex.Message);
            }
        }
    }
}
