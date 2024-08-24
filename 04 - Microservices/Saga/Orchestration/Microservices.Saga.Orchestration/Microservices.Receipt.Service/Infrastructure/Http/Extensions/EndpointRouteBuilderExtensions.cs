using Microservices.Receipt.Service.Infrastructure.Http.EndpointHandlers;
using Microsoft.AspNetCore.Authorization;

namespace Microservices.Receipt.Service.Infrastructure.Http.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        private static void RegisterReceiptEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var authorsEndpoints = endpointRouteBuilder
                .MapGroup("api/receipts")
                .WithTags("ReceiptIds");

            authorsEndpoints.MapGet("/{accountId:guid}", ReceiptsHandlers.GetReceiptsByReceiptIdAsync)
                .WithName("GetReceiptsByReceiptId")
                .WithOpenApi();
        }
        public static void RegisterEndpoints(this IEndpointRouteBuilder app)
        {            
            app.RegisterReceiptEndpoints();         
        }
    }
}
