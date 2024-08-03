﻿using AutoMapper;
using Net.Microservices.CleanArchitecture.Core.Domain;
using Net.Microservices.CleanArchitecture.Core.Domain.Entities.OrderAggregate;
using Net.Microservices.CleanArchitecture.Infrastructure.Models;

namespace Net.Microservices.CleanArchitecture.Infrastructure.Mappings
{
    internal class OrderAddressResolver : IValueResolver<OrderEntity, Order, Address>
    {
        public Address Resolve(OrderEntity source, Order destination, Address destMember, ResolutionContext context) {
            return new Address()
            {
                City = source.AddressCity,
                Country = source.AddressCountry,
                Postcode = source.AddressPostcode,
                Line1 = source.AddressLine1,
                Line2 = source.AddressLine2
            };
        }
    }
}