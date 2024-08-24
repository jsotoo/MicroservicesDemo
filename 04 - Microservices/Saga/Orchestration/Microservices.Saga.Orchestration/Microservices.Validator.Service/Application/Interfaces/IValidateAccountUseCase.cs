
using Microservices.Messages.Enums;
using Microservices.Validator.Service.Core.Results;

namespace Microservices.Validator.Service.Application.Interfaces
{
    public interface IValidateAccountUseCase
    {
        Task<ValidateAccountResult> ExecuteAsync(Guid transactionId, Guid accountId, decimal amount, TransferType type);
    }

}
