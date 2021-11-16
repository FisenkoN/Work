using System;
using Newtonsoft.Json;

namespace School.WEB.Extensions
{
    [Serializable]
    public class OperationResult
    {
        [JsonProperty]
        public string Message { get; set; }
        
        [JsonProperty]
        public bool IsSuccess { get; set; }
        
        public OperationResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }
}