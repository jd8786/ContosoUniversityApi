using ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Validators
{
    public interface ICommonValidator
    {
        IIdValidator IdValidator { get; set; }

        void ValidateCourseChildren(Course course);

        void ValidateStudentChildren(Student student);
    }
}
