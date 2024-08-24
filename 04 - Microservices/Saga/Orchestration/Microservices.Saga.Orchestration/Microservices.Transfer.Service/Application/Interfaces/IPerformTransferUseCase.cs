
using Microservices.Messages.Enums;
using Microservices.Transfer.Service.Core.Results;

namespace Microservices.Transfer.Service.Application.Interfaces
{
    public interface IPerformTransferUseCase
    {
        Task<PerformTransferResult> ExecuteAsync(Guid transactionId, Guid accountId, decimal amount, TransferType type);
    }

}
