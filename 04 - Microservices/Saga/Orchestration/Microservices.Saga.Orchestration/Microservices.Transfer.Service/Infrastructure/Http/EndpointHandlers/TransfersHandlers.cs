using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Transfer.Service.Infrastructure.Http.EndpointHandlers
{
    public static class TransfersHandlers
    {
        public static async Task<Results<Ok<bool>, BadRequest>> GetTransfersByAccountIdAsync(
            Guid accountId
        )
        {            

            return TypedResults.Ok(true);
        }        
    }
}
