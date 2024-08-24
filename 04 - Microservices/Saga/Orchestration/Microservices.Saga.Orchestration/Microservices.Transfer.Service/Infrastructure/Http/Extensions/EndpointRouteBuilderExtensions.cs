using Microservices.Transfer.Service.Infrastructure.Http.EndpointHandlers;
using Microsoft.AspNetCore.Authorization;

namespace Microservices.Transfer.Service.Infrastructure.Http.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        private static void RegisterTransfersEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var authorsEndpoints = endpointRouteBuilder
                .MapGroup("api/transfers")
                .WithTags("Transfers");

            authorsEndpoints.MapGet("/{accountId:guid}", TransfersHandlers.GetTransfersByAccountIdAsync)
                .WithName("GetTransfersByAccountId")
                .WithOpenApi();
        }
        public static void RegisterEndpoints(this IEndpointRouteBuilder app)
        {            
            app.RegisterTransfersEndpoints();         
        }
    }
}
