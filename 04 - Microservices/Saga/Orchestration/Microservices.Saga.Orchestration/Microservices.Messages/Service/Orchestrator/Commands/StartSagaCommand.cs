using Microservices.Messages.Base;
using Microservices.Messages.Enums;

namespace Microservices.Messages.Service.Orchestrator.Commands
{
    public class StartSagaCommand : Command
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public TransferType Type { get; set; }
        public StartSagaCommand(Guid accountId, decimal amount, TransferType type)
        {            
            AccountId = accountId;
            Amount = amount;
            Type = type;
        }
    }
}
