using System;
using Microservices.Infrastructure.Crosscutting;
using Newtonsoft.Json;

namespace Microservices.Products.Infrastructure.Notifications
{
    public class ProductReadModelUpdatedEvent : Event
    {
        public ProductReadModelUpdatedEvent(Guid id)
        {
            Id = id;
        }
    }
}
