using System;
namespace Document.Models
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string WarningMessage { get; set; }


        public static Response<T> CreateSuccessResponse(T data)
        {
            return new Response<T>
            {
                Data = data,
                Success = true,
                ErrorMessage = null,
                WarningMessage = null
            };
        }

        public static Response<T> CreateErrorResponse(string errorMessage)
        {
            return new Response<T>
            {
                Data = default,
                Success = false,
                WarningMessage = null,
                ErrorMessage = errorMessage
            };
        }
        public static Response<T> CreateWarningResponse(string warningMessage, T data = default)
        {
            return new Response<T>
            {
                Data = data,
                Success = true,
                WarningMessage = warningMessage,
                ErrorMessage = null
            };
        }

    }
}

