using Microservices.Infrastructure.Kafka.Producer;
using Microservices.Messages.Enums;
using Microservices.Messages.Service.Validator.Events;
using Microservices.Validator.Service.Application.Interfaces;
using Microservices.Validator.Service.Core.Results;
using Microservices.Validator.Service.Core.Utilities;
using Microservices.Validator.Service.Domain.Interfaces;
using Microservices.Validator.Service.Infrastructure.Data.Repositories;
using static Microservices.Validator.Service.Domain.Entities.Account;

namespace Microservices.Validator.Service.Application.UseCases
{
    public class ValidateAccountUseCase : IValidateAccountUseCase
    {
        private readonly IMessageProducer _messageProducer;
        private readonly ILogger<ValidateAccountUseCase> _logger;
        private readonly IFinancialUnitOfWork _unitOfWork;

        public ValidateAccountUseCase(
            IMessageProducer messagePublisher, 
            ILogger<ValidateAccountUseCase> logger,
            IFinancialUnitOfWork unitOfWork
        )
        {
            _messageProducer = messagePublisher;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ValidateAccountResult> ExecuteAsync(Guid transactionId,Guid accountId,decimal amount, TransferType type)
        {
            try
            {
                var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId);
                if (account == null)
                {
                    await _messageProducer.SendMessageAsync(new AccountValidationFailedEvent(transactionId, accountId, "Account not found"));
                    return new ValidateAccountResult(false, "Account not found");
                }

                if (!account.Validate(amount,type))
                {
                    await _messageProducer.SendMessageAsync(new AccountValidationFailedEvent(transactionId, accountId, "Account validation failed"));
                    return new ValidateAccountResult(false, "Account validation failed");
                }

                await _messageProducer.SendMessageAsync(new AccountValidatedEvent(transactionId, accountId, amount, type));
                return new ValidateAccountResult(true);
            }
            catch (Exception ex)
            {
                await _messageProducer.SendMessageAsync(new AccountValidationFailedEvent(transactionId, accountId, ex.Message));
                return new ValidateAccountResult(false, ex.Message);
            }
        }
    }
}
