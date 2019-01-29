using ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Validators
{
    public interface ICourseValidator
    {
        void ValidateById(int id);

        void ValidatePostCourse(Course course);

        void ValidatePutCourse(Course course);
    }
}
