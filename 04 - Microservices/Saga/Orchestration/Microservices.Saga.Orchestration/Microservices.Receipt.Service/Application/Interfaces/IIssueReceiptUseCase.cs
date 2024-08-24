
using Microservices.Messages.Enums;
using Microservices.Receipt.Service.Core.Results;

namespace Microservices.Receipt.Service.Application.Interfaces
{
    public interface IIssueReceiptUseCase
    {
        Task<IssueReceiptResult> ExecuteAsync(Guid transactionId, Guid accountId, decimal amount, TransferType type);
    }

}
