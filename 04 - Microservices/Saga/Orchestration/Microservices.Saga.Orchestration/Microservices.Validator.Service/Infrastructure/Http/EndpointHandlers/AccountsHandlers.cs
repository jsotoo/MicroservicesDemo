using Microservices.Infrastructure.Kafka.Producer;
using Microservices.Messages.Service.Validator.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Validator.Service.Infrastructure.Http.EndpointHandlers
{
    public static class AccountsHandlers
    {
        public static async Task<Results<Ok<bool>, BadRequest>> ValidateAccountAsync(
            [FromServices] IMessageProducer _messageProducer,
            Guid accountId
        )
        {            

            return TypedResults.Ok(true);
        }        
    }
}
