using Microservices.Validator.Service.Infrastructure.Http.EndpointHandlers;
using Microsoft.AspNetCore.Authorization;

namespace Microservices.Saga.Orchestrator.Service.Infrastructure.Http.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        private static void RegisterSagasEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var authorsEndpoints = endpointRouteBuilder
                .MapGroup("api/sagas")
                .WithTags("Sagas");

            authorsEndpoints.MapGet("/{transactionId:guid}", SagasHandlers.GetSagaByTransactionIdAsync)
                .WithName("GetSagaByTransactionId")
                .WithOpenApi();

            authorsEndpoints.MapPost("", SagasHandlers.StartTransactionAsync)
                .WithName("StartTransaction")
                .WithOpenApi();
        }
        public static void RegisterEndpoints(this IEndpointRouteBuilder app)
        {
            app.RegisterSagasEndpoints();
        }
    }
}
