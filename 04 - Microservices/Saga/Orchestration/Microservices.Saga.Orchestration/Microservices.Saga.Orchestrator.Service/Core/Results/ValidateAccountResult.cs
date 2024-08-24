namespace Microservices.Saga.Orchestrator.Service.Core.Results
{
    public class ValidateAccountResult
    {
        public bool Success { get; }
        public Guid TransactionId { get; }
        public string Message { get; }
        public ValidateAccountResult(bool success,Guid transactionId, string message = "")
        {
            Success = success;
            TransactionId = transactionId;
            Message = message;
        }
    }
}
