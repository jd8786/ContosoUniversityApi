namespace ContosoUniversity.Models
{
    public class ApiResponseWithData<T>: ApiResponseBase
    {
        public T Data { get; set; }

        public static ApiResponseBase Success(T data)
        {
            return new ApiResponseWithData<T>()
            {
                IsSuccess = true,
                Data = data
            };
        }

        public static ApiResponseBase Error(T data, string message)
        {
            return new ApiResponseWithData<T>()
            {
                IsSuccess = false,
                Data = data,
                Message = message
            };
        }
    }
}
