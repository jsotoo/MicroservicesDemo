using Microservices.Infrastructure.Kafka.Producer;
using Microservices.Messages.Enums;
using Microservices.Messages.Service.Receipt.Events;
using Microservices.Messages.Service.Transfer.Events;
using Microservices.Messages.Service.Validator.Events;
using Microservices.Receipt.Service.Application.Interfaces;
using Microservices.Receipt.Service.Core.Results;
using Microservices.Receipt.Service.Domain.Interfaces;
using Microservices.Receipt.Service.Infrastructure.Persistence.Repositories;

namespace Microservices.Receipt.Service.Application.UseCases
{
    public class IssueReceiptUseCase : IIssueReceiptUseCase
    {
        private readonly IMessageProducer _messageProducer;
        private readonly ILogger<IssueReceiptUseCase> _logger;
        private readonly IReceiptUnitOfWork _unitOfWork;

        public IssueReceiptUseCase(
            IMessageProducer messagePublisher, 
            ILogger<IssueReceiptUseCase> logger,
            IReceiptUnitOfWork unitOfWork
        )
        {
            _messageProducer = messagePublisher;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IssueReceiptResult> ExecuteAsync(Guid transactionId, Guid accountId, decimal amount,TransferType type)
        {
            var receipt = new Domain.Entities.Receipt(Guid.NewGuid(), accountId, DateTime.Now, amount, type);
            receipt = await _unitOfWork.ReceiptRepository.CreateAsync(receipt);
            await _unitOfWork.CommitAsync();

            await _messageProducer.SendMessageAsync(new ReceiptIssuedEvent(transactionId, accountId, amount,type));

            return new IssueReceiptResult(true);
        }
    }
}
