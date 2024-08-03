using System.Threading.Tasks;
using Net.Microservices.CleanArchitecture.Common;
using Net.Microservices.CleanArchitecture.Core.Domain.Entities.OrderAggregate;

namespace Net.Microservices.CleanArchitecture.Core.Application.Commands
{
    public record CreateNewOrderCommand(string TrackingNumber) : IRequest<Result>
    {
    }

    /// <summary>
    /// Create New Order Command Handler
    /// </summary>
    public class CreateNewOrderCommandHandler : BaseMessageHandler<CreateNewOrderCommand, Result>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateNewOrderCommandHandler(IOrderRepository orderRepository) {
            _orderRepository = orderRepository;
        }

        public override async Task<Result> HandleAsync(CreateNewOrderCommand command) {
            Order order = new Order(command.TrackingNumber);
            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveToEventStoreAsync(order);
            await _orderRepository.UnitOfWork.SaveChangesAsync();
            return new Result().Successful();
        }
    }
}
