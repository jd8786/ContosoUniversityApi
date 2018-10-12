using System.Collections.Generic;

namespace ContosoUniversity.Api.Models
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }

        public List<string> Messages { get; set; }

        public T Data { get; set; }

        public static ApiResponse<T> Success(T data)
        {
            return new ApiResponse<T>
            {
                IsSuccess = true,

                Data = data
            };
        }

        public static ApiResponse<T> Error(string message)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,

                Messages = new List<string> { message }
            };
        }

        public static ApiResponse<T> Error(List<string> messages)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,

                Messages = messages
            };
        }
    }
}
