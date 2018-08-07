using System.Collections.Generic;

namespace ContosoUniversity.Api.Models
{
    public class ApiResponseOfStudents: IApiResponse
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public IEnumerable<StudentInfo> Students { get; set; }

        public static ApiResponseOfStudents Success(IEnumerable<StudentInfo> students, string message = "")
        {
            return new ApiResponseOfStudents
            {
                IsSuccess = true,
                Students = students,
                Message = message
            };
        }

        public static ApiResponseOfStudents Error(string message)
        {
            return new ApiResponseOfStudents
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}
