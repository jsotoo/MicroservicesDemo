using Microservices.Messages.Base;
using Microservices.Messages.Enums;

namespace Microservices.Messages.Service.Transfer.Commands
{
    public class PerformTransferCommand : Command
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public TransferType Type { get; set; }

        public PerformTransferCommand(Guid transactionId, Guid accountId, decimal amount,TransferType type)
        {
            TransactionId = transactionId;
            AccountId = accountId;
            Amount = amount;
            Type = type;
        }
    }
}
