using ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Validators
{
    public interface ICourseValidator
    {
        ICommonValidator CommonValidator { get; set; }

        void ValidatePostCourse(Course course);

        void ValidatePutCourse(Course course);
    }
}
