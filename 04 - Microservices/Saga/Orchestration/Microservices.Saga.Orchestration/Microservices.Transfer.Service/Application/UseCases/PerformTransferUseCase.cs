using Microservices.Infrastructure.Kafka.Producer;
using Microservices.Messages.Enums;
using Microservices.Messages.Service.Transfer.Events;
using Microservices.Messages.Service.Validator.Events;
using Microservices.Transfer.Service.Application.Interfaces;
using Microservices.Transfer.Service.Core.Results;
using Microservices.Transfer.Service.Domain.Interfaces;
using Microservices.Transfer.Service.Infrastructure.Data.Repositories;

namespace Microservices.Transfer.Service.Application.UseCases
{
    public class PerformTransferUseCase : IPerformTransferUseCase
    {
        private readonly IMessageProducer _messageProducer;
        private readonly ILogger<PerformTransferUseCase> _logger;
        private readonly ITransferUnitOfWork _unitOfWork;

        public PerformTransferUseCase(
            IMessageProducer messagePublisher, 
            ILogger<PerformTransferUseCase> logger,
            ITransferUnitOfWork unitOfWork
        )
        {
            _messageProducer = messagePublisher;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<PerformTransferResult> ExecuteAsync(Guid transactionId, Guid accountId, decimal amount,TransferType type)
        {
            var transfer = new Domain.Entities.Transfer(Guid.NewGuid(), accountId, DateTime.Now, amount, type);
            transfer = await _unitOfWork.TransferRepository.CreateAsync(transfer);
            await _unitOfWork.CommitAsync();

            await _messageProducer.SendMessageAsync(new TransferredEvent(transactionId, accountId, amount,type));

            return new PerformTransferResult(true);
        }
    }
}
