using AutoMapper;
using Net.Microservices.CleanArchitecture.Core.Application.ReadModels;
using Net.Microservices.CleanArchitecture.Core.Application.ReadModels.Orders;
using Net.Microservices.CleanArchitecture.Core.Domain;
using Net.Microservices.CleanArchitecture.Core.Domain.Entities.OrderAggregate;

namespace Net.Microservices.CleanArchitecture.Core.Application.Mappings
{
    internal class OrderProfile : Profile
    {
        public OrderProfile() {
            CreateMap<Order, OrderReadModel>();
            CreateMap<OrderItem, OrderItemReadModel>();
            CreateMap<Address, AddressReadModel>();
        }
    }
}
