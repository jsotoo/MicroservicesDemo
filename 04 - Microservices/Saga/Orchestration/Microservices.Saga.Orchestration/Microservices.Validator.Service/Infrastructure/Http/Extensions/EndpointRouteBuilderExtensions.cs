using Microservices.Validator.Service.Infrastructure.Http.EndpointHandlers;
using Microsoft.AspNetCore.Authorization;

namespace Microservices.Validator.Service.Infrastructure.Http.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        private static void RegisterAccountsEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var authorsEndpoints = endpointRouteBuilder
                .MapGroup("api/accounts")
                .WithTags("Accounts");

            authorsEndpoints.MapPost("/{accountId:guid}", AccountsHandlers.ValidateAccountAsync)
                .WithName("ValidateAccount")
                .WithOpenApi();
        }
        public static void RegisterEndpoints(this IEndpointRouteBuilder app)
        {            
            app.RegisterAccountsEndpoints();         
        }
    }
}
