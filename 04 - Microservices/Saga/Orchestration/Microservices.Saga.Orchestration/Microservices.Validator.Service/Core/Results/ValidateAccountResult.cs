namespace Microservices.Validator.Service.Core.Results
{
    public class ValidateAccountResult
    {
        public bool Success { get; }
        public string Message { get; }
        public ValidateAccountResult(bool success, string message = "")
        {                
            Success = success;
            Message = message;         
        }
    }
}
