using System;
using System.Threading.Tasks;
using Net.Microservices.CleanArchitecture.Core.Domain.Entities.OrderAggregate;

namespace Net.Microservices.CleanArchitecture.Core.Application
{
    public interface IOrderRepository : IAggregateRepository<Order, Guid>
    {
        Task SaveToEventStoreAsync(Order order);
    }
}