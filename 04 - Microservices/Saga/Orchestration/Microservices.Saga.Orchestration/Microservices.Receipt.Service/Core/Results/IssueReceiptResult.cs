namespace Microservices.Receipt.Service.Core.Results
{
    public class IssueReceiptResult
    {
        public bool Success { get; }
        public string Message { get; }
        public IssueReceiptResult(bool success, string message = "")
        {                
            Success = success;
            Message = message;         
        }
    }
}
