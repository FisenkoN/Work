using System;
using Newtonsoft.Json;

namespace School.WEB.Extensions
{
    [Serializable]
    public class OperationResult<TResult>
    {
        private OperationResult ()
        {
        }

        [JsonProperty]
        public bool Success { get; private set; }
        
        [JsonProperty]
        public TResult Result { get; private set; }

        public static OperationResult<TResult> CreateSuccessResult(TResult result)
        {
            return new OperationResult<TResult>
            {
                Success = true, Result = result
            };
        }

        public static OperationResult<TResult> CreateFailure(TResult nonSuccessMessage)
        {
            return new OperationResult<TResult>
            {
                Success = false, Result = nonSuccessMessage
            };
        }
    }
}