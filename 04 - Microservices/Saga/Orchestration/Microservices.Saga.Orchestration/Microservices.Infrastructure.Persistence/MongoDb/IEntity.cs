using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Infrastructure.Persistence.MongoDb
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }

}
