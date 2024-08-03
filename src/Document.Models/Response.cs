using System;
namespace Document.Models
{
	public class Response<T>
	{
        public T Data { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public static Response<T> CreateSuccessResponse(T data)
        {
            return new Response<T>
            {
                Data = data,
                Success = true,
                ErrorMessage = null
            };
        }

        public static Response<T> CreateErrorResponse(string errorMessage)
        {
            return new Response<T>
            {
                Data = default,
                Success = false,
                ErrorMessage = errorMessage
            };
        }
    }
}

