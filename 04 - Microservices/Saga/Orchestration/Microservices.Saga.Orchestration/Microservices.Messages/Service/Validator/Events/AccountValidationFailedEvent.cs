using Microservices.Messages.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Messages.Service.Validator.Events
{
    public class AccountValidationFailedEvent : Event
    {        
        public AccountValidationFailedEvent(Guid transactionId,Guid accountId,string reason)
        {
            TransactionId = transactionId;
            AccountId = accountId;
            Reason = reason;
        }
        public Guid AccountId { get; set; }
        public string Reason { get; set; }
    }

}
