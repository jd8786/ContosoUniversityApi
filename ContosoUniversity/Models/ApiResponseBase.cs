namespace ContosoUniversity.Models
{
    public class ApiResponseBase
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public static ApiResponseBase Success()
        {
            return new ApiResponseBase()
            {
                IsSuccess = true,
            };
        }

        public static ApiResponseBase Error(string message)
        {
            return new ApiResponseBase ()
            {
                IsSuccess = false,
                Message = message
            };
        }

    }
}
