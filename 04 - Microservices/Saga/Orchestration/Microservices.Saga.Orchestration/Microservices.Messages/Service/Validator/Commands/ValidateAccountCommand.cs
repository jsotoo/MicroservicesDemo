using Microservices.Messages.Base;
using Microservices.Messages.Enums;
using System.Diagnostics;

namespace Microservices.Messages.Service.Validator.Commands
{
    public class ValidateAccountCommand: Command
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public TransferType Type { get; set; }

        public ValidateAccountCommand(Guid transactionId,Guid accountId, decimal amount, TransferType type)
        {
            TransactionId = transactionId;
            AccountId = accountId;
            Amount = amount;
            Type = type;
        }
    }
}
