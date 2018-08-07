namespace ContosoUniversity.Api.Models
{
    public class ApiResponseOfStudentInfo: IApiResponse
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public StudentInfo StudentInfo { get; set; }

        public static ApiResponseOfStudentInfo Success(StudentInfo studentInfo, string message = "")
        {
            return new ApiResponseOfStudentInfo
            {
                IsSuccess = true,
                StudentInfo = studentInfo,
                Message = message
            };
        }

        public static ApiResponseOfStudentInfo Error(string message)
        {
            return new ApiResponseOfStudentInfo
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}
