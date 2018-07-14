namespace ContosoUniversity.Models
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public static ApiResponse<T> Success(T data)
        {
            return new ApiResponse<T>()
            {
                IsSuccess = true,
                Data = data
            };
        }

        public static ApiResponse<T> Error(T data, string message)
        {
            return new ApiResponse<T>()
            {
                IsSuccess = false,
                Data = data,
                Message = message
            };
        }
    }
}
