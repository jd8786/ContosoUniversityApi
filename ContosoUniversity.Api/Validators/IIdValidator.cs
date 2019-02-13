namespace ContosoUniversity.Api.Validators
{
    public interface IIdValidator
    {
        void ValidateStudentById(int studentId);

        void ValidateCourseById(int courseId);

        void ValidateDepartmentById(int departmentId);

        void ValidateInstructorById(int instructorId);
    }
}
