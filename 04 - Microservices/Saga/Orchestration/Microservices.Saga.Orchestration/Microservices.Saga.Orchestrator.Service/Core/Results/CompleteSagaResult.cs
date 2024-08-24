namespace Microservices.Saga.Orchestrator.Service.Core.Results
{
    public class CompleteSagaResult
    {
        public bool Success { get; }
        public Guid TransactionId { get; }
        public string Message { get; }
        public CompleteSagaResult(bool success,Guid transactionId, string message = "")
        {
            Success = success;
            TransactionId = transactionId;
            Message = message;
        }
    }
}
