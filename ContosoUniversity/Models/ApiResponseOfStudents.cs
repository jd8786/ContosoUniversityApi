using System.Collections.Generic;
using ContosoUniversity.Data.Models;

namespace ContosoUniversity.Models
{
    public class ApiResponseOfStudents: IApiResponse
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public IEnumerable<Student> Students { get; set; }

        public static ApiResponseOfStudents Success(IEnumerable<Student> students, string message = "")
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
