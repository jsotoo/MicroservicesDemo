﻿namespace Microservices.Validator.Service.Core.Utilities
{
    public class OperationResult
    {
        public bool Success { get; }
        public string Message { get; }
        public object Data { get; }

        public OperationResult(bool success, string message = "", object data = null)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }

}
