﻿
namespace Microservice.CQRS.Deluxe.Infrastructure.Framework
{
    public class Message
    {
        public string SagaId { get; protected set; }
        public string Name { get; protected set; }
    }
}