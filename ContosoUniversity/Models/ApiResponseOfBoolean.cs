namespace ContosoUniversity.Models
{
    public class ApiResponseOfBoolean: IApiResponse
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public static ApiResponseOfBoolean Success(string message = "")
        {
            return new ApiResponseOfBoolean
            {
                IsSuccess = true,
                Message = message
            };
        }

        public static ApiResponseOfBoolean Error(string message)
        {
            return new ApiResponseOfBoolean
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}
