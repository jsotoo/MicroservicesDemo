using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Receipt.Service.Infrastructure.Http.EndpointHandlers
{
    public static class ReceiptsHandlers
    {
        public static async Task<Results<Ok<bool>, BadRequest>> GetReceiptsByReceiptIdAsync(
            Guid receiptId
        )
        {            

            return TypedResults.Ok(true);
        }        
    }
}
