using ContosoUniversity.Data.Models;

namespace ContosoUniversity.Models
{
    public class ApiResponseOfStudent: IApiResponse
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public Student Student { get; set; }

        public static ApiResponseOfStudent Success(Student student, string message = "")
        {
            return new ApiResponseOfStudent
            {
                IsSuccess = true,
                Student = student,
                Message = message
            };
        }

        public static ApiResponseOfStudent Error(string message)
        {
            return new ApiResponseOfStudent
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}
