using System.Collections.Generic;

namespace ContosoUniversity.Api.Models
{
    public class ApiResponseOfStudentInfos: IApiResponse
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public IEnumerable<StudentInfo> StudentInfos { get; set; }

        public static ApiResponseOfStudentInfos Success(IEnumerable<StudentInfo> studentInfos, string message = "")
        {
            return new ApiResponseOfStudentInfos
            {
                IsSuccess = true,
                StudentInfos = studentInfos,
                Message = message
            };
        }

        public static ApiResponseOfStudentInfos Error(string message)
        {
            return new ApiResponseOfStudentInfos
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}
