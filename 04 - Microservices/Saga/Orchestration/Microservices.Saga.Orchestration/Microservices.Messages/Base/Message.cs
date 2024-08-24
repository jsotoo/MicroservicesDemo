using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Messages.Base
{
    public abstract class Message: IRequest
    {
        public Guid TransactionId { get; set; }        
    }
}
