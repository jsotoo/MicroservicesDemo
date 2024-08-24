using Microservices.Messages.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Messages.Service.Orchestrator.Events
{
    public class SagaCompletedEvent : Event
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Details { get; set; }

        public SagaCompletedEvent(Guid transactionId, Guid accountId, decimal amount, string details = "")
        {
            TransactionId = transactionId;
            AccountId = accountId;
            Amount = amount;
            Details = details;
        }

    }

}
