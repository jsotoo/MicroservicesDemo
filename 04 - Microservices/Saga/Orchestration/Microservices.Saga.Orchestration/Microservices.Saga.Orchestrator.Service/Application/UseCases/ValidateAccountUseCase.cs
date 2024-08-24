
using Microservices.Infrastructure.Kafka.Producer;
using Microservices.Messages.Service.Validator.Commands;
using Microservices.Messages.Service.Validator.Events;
using Microservices.Saga.Orchestrator.Service.Application.Interfaces;
using Microservices.Saga.Orchestrator.Service.Core.Results;
using Microservices.Saga.Orchestrator.Service.Domain.Entities;
using Microservices.Saga.Orchestrator.Service.Domain.Interfaces;

namespace Microservices.Saga.Orchestrator.Service.Application.UseCases
{
    public class ValidateAccountUseCase : IValidateAccountUseCase
    {
        private readonly IMessageProducer _messageProducer;
        private readonly ILogger<ValidateAccountUseCase> _logger;
        private readonly IOrchestratorUnitOfWork _unitOfWork;

        public ValidateAccountUseCase(
            IMessageProducer messageProducer,
            ILogger<ValidateAccountUseCase> logger,
            IOrchestratorUnitOfWork unitOfWork
        )
        {
            _messageProducer = messageProducer;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ValidateAccountResult> ExecuteAsync(Domain.Entities.Saga saga)
        {
            try
            {
                await _messageProducer.SendMessageAsync(new ValidateAccountCommand(saga.TransactionId, saga.AccountId, saga.Amount, saga.TransferType));
                return new ValidateAccountResult(true,saga.TransactionId);
            }
            catch (Exception ex)
            {                
                return new ValidateAccountResult(false,saga.TransactionId, ex.Message);
            }
        }
    }
}
