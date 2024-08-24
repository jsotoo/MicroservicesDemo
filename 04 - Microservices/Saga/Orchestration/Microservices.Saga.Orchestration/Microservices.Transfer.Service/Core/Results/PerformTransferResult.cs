namespace Microservices.Transfer.Service.Core.Results
{
    public class PerformTransferResult
    {
        public bool Success { get; }
        public string Message { get; }
        public PerformTransferResult(bool success, string message = "")
        {                
            Success = success;
            Message = message;         
        }
    }
}
