using Microservices.Infrastructure.Kafka.Producer;
using Microservices.Messages.Enums;
using Microservices.Messages.Service.Orchestrator.Commands;
using Microservices.Saga.Orchestrator.Service.Infrastructure.Http.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Validator.Service.Infrastructure.Http.EndpointHandlers
{
    public static class SagasHandlers
    {
        public static async Task<Results<Ok<bool>, BadRequest>> GetSagaByTransactionIdAsync(
            Guid transactionId
        )
        {            

            return TypedResults.Ok(true);
        }
        public static async Task<Results<Ok<bool>, BadRequest>> StartTransactionAsync(
            [FromServices] IMessageProducer _messageProducer            
        )
        {
            _messageProducer.SendMessageAsync(new StartSagaCommand(Guid.Parse("f8b09e51-2959-4c88-ba6a-76dcbbb01aca"), 100, TransferType.Debit));
            return TypedResults.Ok(true);
        }
    }
}
