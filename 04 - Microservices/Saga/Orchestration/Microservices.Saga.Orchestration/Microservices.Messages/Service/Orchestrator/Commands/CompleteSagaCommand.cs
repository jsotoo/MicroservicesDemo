using Microservices.Messages.Base;

namespace Microservices.Messages.Service.Orchestrator.Commands
{
    public class CompleteSagaCommand : Command
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }

        public CompleteSagaCommand(Guid transactionId, Guid accountId, decimal amount)
        {
            TransactionId = transactionId;
            AccountId = accountId;
            Amount = amount;
        }
    }
}
