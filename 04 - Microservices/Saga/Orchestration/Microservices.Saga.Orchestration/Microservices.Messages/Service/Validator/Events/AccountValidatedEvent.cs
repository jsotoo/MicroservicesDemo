using Microservices.Messages.Base;
using Microservices.Messages.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Messages.Service.Validator.Events
{
    public class AccountValidatedEvent : Event
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Details { get; set; }
        public TransferType Type { get; set; }
        
        public AccountValidatedEvent(Guid transactionId, Guid accountId,decimal amount, TransferType type, string details="")
        {
            TransactionId = transactionId;
            AccountId = accountId;
            Amount = amount;
            Type = type;
            Details = details;            
        }
        
    }

}
